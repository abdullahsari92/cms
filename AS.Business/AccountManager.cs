using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Models;
using AS.Entities.Simple;
using AutoMapper;
using Core.Extensions;
using Core.Utilities.Security.Hashing;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class AccountManager : IAccountService
    {

        private IRepository<User> _userRepository;

        IRepository<Person> _personRepository;
        protected IMapper _mapper;

        public AccountManager(IRepository<User> userRepository, IRepository<Person> repository, IMapper mapper)
        {
            _userRepository = userRepository;
            _personRepository = repository;
            _mapper = mapper;
        }


        public async Task<PersonDto> GetProfile(CancellationToken token)
        {
            var userId = UserInfoExtensions.GetUserId();
            var unitId = UserInfoExtensions.GetUnitId();
            var userType = UserInfoExtensions.GetUserType();

            //todo: Student tipindeki kullanýcý için farklý bir method yapýlmasý lazým. solide uygun deðil.
            PersonDto personDto = new();
            if (userType != (int)UserType.Student)
            {
                Person person = new();

                var query = await _personRepository.GetAll(p => p.UserId == userId);
               person = await query.Include(p => p.User)
                .ThenInclude(p => p.RoleUserLines)
                .ThenInclude(p => p.Role)
                .Include(p => p.User.RoleUserLines).ThenInclude(p => p.Units)
                    .FirstOrDefaultAsync(token);

                personDto = _mapper.Map(person, new PersonDto());

                personDto.RoleDepartments = person.User.RoleUserLines.Where(m => m.UnitId != null).Select(m => m.Units).Distinct()
                                                    .Select(p => new RoleDepartmentModel
                                                    {
                                                        UnitId = p.Id,
                                                        IsDefaultDepartment = p.RoleUserLineList.FirstOrDefault(m => m.UnitId == p.Id).IsDefaultDepartment,
                                                        RoleNames = p.RoleUserLineList.Where(m => m.UnitId == p.Id).Select(m => m.Role.Name.ToString()).ToList(),
                                                        RoleIds = p.RoleUserLineList.Where(m => m.UnitId != null).Select(p => p.RoleId.ToString()).ToList(),
                                                        UnitsName = p.Name,
                                                         
                                                    }).Distinct().ToList();

                var notDepartmentRoles = person.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => new RoleDepartmentModel
                {
                    IsDefaultDepartment = p.IsDefaultDepartment,
                    RoleIds = p.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => p.RoleId.ToString()).ToList(),
                    RoleNames = p.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => p.Role.Name.ToString()).ToList(),
                }).FirstOrDefault();

                if (notDepartmentRoles != null) personDto.RoleDepartments.Add(notDepartmentRoles);
            }
            else
            {        
            }
               


            return personDto;
        }

  

        public async Task PasswordChange(PasswordChangeModel model)
        {
            var userId = UserInfoExtensions.GetUserId();

            User? identityUser = await _userRepository.GetAsync(p => p.Id == userId);

            if (identityUser == null)
            {
                throw new NotFoundException();
            }

            if (!identityUser.IsApproved)
            {
                throw new NotApprovedException();
            }


            // Þifresi yanlýþ ise
            if (HashingHelper.VerifyPasswordHash(model.OldPassword.Trim()) != identityUser.Password.Trim())
            {
                throw new CustomException("Eski þifre yanlýþ");
            }

            //þifre tekrar giriniz
            if (model.Password != model.ConfirmPassword)
            {
                throw new CustomException("Ýlk þifreyle ikinci þifre ayný deðil");
            }

            identityUser.Password = HashingHelper.VerifyPasswordHash(model.Password);

            await _userRepository.UpdateAsync(identityUser);


        }

        public async Task<PersonDto> UpdateProfile(PersonDto personDto)
        {

            var query = await _personRepository.GetAll(p => p.Id == personDto.Id);

            var person = await query.Include(p => p.User).FirstOrDefaultAsync();
            var user = person.User;
            //var user = await _userRepository.GetAsync(p => p.Id == personDto.UserId);

            user.Username = personDto.Username;
            user.Email = personDto.Email;

            //var userUpdate = await _userRepository.UpdateAsync(user);



            person = _mapper.Map(personDto, person);

            person = BaseEntityHelper.SetBaseUpdateEntitiy(person);

            person.User = user;

            return _mapper.Map(await _personRepository.UpdateAsync(person), new PersonDto());




        }

        public async Task<PersonDto> GetById(Guid id)
        {
            var query = await _personRepository.GetAll(p => p.Id == id);

            var entity = query.Include(p => p.User).ThenInclude(p => p.RoleUserLines).ThenInclude(m => m.Role)
                              .Include(p => p.User)
                              .ThenInclude(p => p.RoleUserLines)
                              .ThenInclude(m => m.Units)
                              .FirstOrDefault();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var personDto = _mapper.Map(entity, new PersonDto());

            personDto.RoleDepartments = entity.User.RoleUserLines.Where(m => m.UnitId != null).Select(m => m.Units).Distinct()
                                                .Select(p => new RoleDepartmentModel
                                                {
                                                    UnitId = p.Id,
                                                    IsDefaultDepartment = p.RoleUserLineList.FirstOrDefault(m => m.UnitId == p.Id).IsDefaultDepartment,
                                                    RoleIds = p.RoleUserLineList.Where(m => m.UnitId == p.Id).Select(m => m.RoleId.ToString()).ToList(),
                                                    RoleNames = p.RoleUserLineList.Where(m => m.UnitId == p.Id).Select(m => m.Role.Name.ToString()).ToList(),
                                                    UnitsName = p.Name,
                                                 
                                                }).Distinct().ToList();

            return personDto;
        }
        public async Task<List<NameValue>> GetSelectOptionsDepartmans(string email)
        {
            var query = await _userRepository.GetAll(p => p.Email == email);

            var entity = query.Include(p => p.RoleUserLines)
                              .ThenInclude(m => m.Units)
                              .FirstOrDefault();


            var listModel = entity.RoleUserLines.Where(m => m.UnitId != null).Select(m => m.Units).Distinct()
                       .Select(p => new NameValue
                       {
                           Name = p.Name,
                           Value = p.Id.ToString(),
                           Selected = p.RoleUserLineList
                                            .Any(r => r.UnitId == p.Id && r.IsDefaultDepartment)
                       })
                       .ToList();

            return listModel;
        }

  
    }
}


