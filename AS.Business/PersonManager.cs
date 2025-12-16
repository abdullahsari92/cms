using AS.Business.EmailMessage;
using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Core.Utilities.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AS.Business
{
    public class PersonManager : BaseManager<Person, PersonDto>, IPersonService
    {


        public readonly IUserService _userService;
        public readonly IRepository<User> _userRepository;
        public readonly IEMailSender _emailSender;
        public IConfiguration _configuration { get; }


        public readonly IRepository<RoleUserLine> _roleUserLineRepository;

        protected readonly string _isUseEmail;

        public PersonManager(IConfiguration configuration, IRepository<Person> repository, IMapper mapper, IRedisService redisService, IUserService userService = null, IRepository<RoleUserLine> roleUserLineRepository = null, IRepository<User> userRepository = null, IEMailSender emailSender = null) : base(repository, mapper, redisService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleUserLineRepository = roleUserLineRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _isUseEmail = _configuration.GetSection("EmailSetting:isUseEmail").Value ?? "";

        }

        #region Private

        private async Task AllRoleDelte(Guid userId)
        {
            var userLineRole = await _roleUserLineRepository.GetAll(p => p.UserId == userId);
            await _roleUserLineRepository.DeleteRangeAsync(userLineRole, true);
        }

        #endregion 
        public async Task<ListModel<PersonDto>> GetAll(CancellationToken token)
        {

            var listModel = new ListModel<PersonDto>();
            // sadece other ve sorumlu ��retim elemanlar�n� listeliyoruz.
            var query = await _repository.GetAll(p => p.User.UserType == (int)UserType.Other || p.User.UserType == (int)UserType.ResponsibleTeacher);

            query = query.Include(p => p.User).Include(p => p.User.RoleUserLines).ThenInclude(p => p.Units);


            var unitId = UserInfoExtensions.GetUnitId();
            var facultyId = UserInfoExtensions.GetFacultyId();

            if (unitId != null)
            {
                var roleLevel = UserInfoExtensions.GetRoleLevel();

                query = query.Where(p => p.User.RoleUserLines.FirstOrDefault(m => m.Units.Id == facultyId).Units.Id == facultyId);

                if (roleLevel > 13)
                {
                    query = query.Where(p => p.User.RoleUserLines.FirstOrDefault(m => m.UnitId == unitId).UnitId == unitId);
                }
            }

            listModel.Items = await query.ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync(token);
               
            return listModel;
        }
        public async Task<PersonDto> GetById(Guid id)
        {
            var query = await _repository.GetAll(p => p.Id == id);

            var entity = query.Include(p => p.User).ThenInclude(p => p.RoleUserLines).ThenInclude(m => m.Role)
                .Include(p => p.User).ThenInclude(p => p.RoleUserLines).ThenInclude(m => m.Units)
                .FirstOrDefault();
            if (entity == null)
            {
                throw new NotFoundException();
            }

            var personDto = _mapper.Map(entity, new PersonDto());



            //  personDto.RoleIds = entity.User.RoleUserLines.Where(m => m.DepartmentId == null).Select(p => p.RoleId.ToString()).ToList();


            personDto.RoleDepartments = entity.User.RoleUserLines.Where(m => m.UnitId != null).Select(p => p.Units).Distinct().Select(p => new RoleDepartmentModel
            {
                UnitId = p.Id,
                IsDefaultDepartment = p.RoleUserLineList.FirstOrDefault(m => m.UnitId == p.Id).IsDefaultDepartment,
                RoleIds = p.RoleUserLineList.Where(m => m.UnitId == p.Id).Select(p => p.RoleId.ToString()).ToList(),
                RoleNames = p.RoleUserLineList.Where(m => m.UnitId == p.Id).Select(p => p.Role.Name.ToString()).ToList(),
                UnitsName = p.Name,

            }).Distinct().ToList();


            var notDepartmentRoles = entity.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => new RoleDepartmentModel
            {
                IsDefaultDepartment = p.IsDefaultDepartment,
                RoleIds = p.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => p.RoleId.ToString()).ToList(),
                RoleNames = p.User.RoleUserLines.Where(m => m.UnitId == null).Select(p => p.Role.Name.ToString()).ToList(),

            }).FirstOrDefault();

            if (notDepartmentRoles != null) personDto.RoleDepartments.Add(notDepartmentRoles);



            //personDto.RoleIds = entity.User.RoleUserLines.Select(p => p.RoleId.ToString()).ToList();

            return personDto;
        }


        public async Task<IDataResult<PersonDto>> Insert(PersonDto personDto)
        {
            if (await _userService.BaseIsExist(p => p.Username == personDto.Username || p.Email == personDto.Email))
                throw new DuplicateException("Bu 'e-mail' veya 'kullan�c� ad�'  daha �nce eklenmi�tir.");

            var randomPassword = SecurityHelper.CreatePassword(8);

            if (!_isUseEmail.ToBoolean()) // todo : burada default olarak kullan�c�ya �ifer gitmedi�in i�in test taraf�na bu kullan�l�yor.
            {
                randomPassword = "test12345";
            }
            UserDto userDto = new UserDto
            {
                Username = personDto.Username,
                Email = personDto.Email,
                //Password = "b+wqlgHVs1gclPIVD8B/o9bkWAgHlCg1S4aOQSt25rs=", // default test12345 isimBa�harfi + tcKimlik+ soyisim  gibi otamatik passwordd olu�turulacak
                Password = HashingHelper.VerifyPasswordHash(randomPassword),
                UserType = personDto.UserType == 0 ? (int)UserType.Other : personDto.UserType,
                Id = new Guid(),
                IsApproved = true
            };

            var person = _mapper.Map(personDto, new Person());
            var user = _mapper.Map(userDto, new User());


            user = BaseEntityHelper.SetBaseEntitiy(user);
            person = BaseEntityHelper.SetBaseEntitiy(person);

            person.Id = Guid.NewGuid();
            person.IsApproved = true;
            person.User = user;
            person.UserId = user.Id;
            person = await _repository.InsertAsync(person);




            var isDefaultDepartmanChecked = personDto.RoleDepartments;

            var isFirstTrue = personDto.RoleDepartments.Where(x => x.IsDefaultDepartment).Count() == 1;

            if (!isFirstTrue)
            {
                isDefaultDepartmanChecked.ForEach(x => x.IsDefaultDepartment = false);
                isDefaultDepartmanChecked.First().IsDefaultDepartment = true;
            }

            List<RoleUserLine> lineList = new();



            foreach (var item in personDto.RoleDepartments)
            {

                foreach (var roleId in item.RoleIds)
                {
                    var roleLine = new RoleUserLine()
                    {
                        UserId = person.UserId,
                        RoleId = new Guid(roleId),
                        Id = Guid.NewGuid(),
                        UnitId = item.UnitId,
                        IsDefaultDepartment = item.IsDefaultDepartment,
                        IsApproved = true
                    };

                    roleLine = BaseEntityHelper.SetBaseEntitiy(roleLine);
                    lineList.Add(roleLine);
                }
            }



            var sonuc = await _roleUserLineRepository.InsertRangeAsync(lineList);

            if (sonuc != 0)
            {

            }

            bool result = _emailSender.Sender(personDto.Email, randomPassword);

            if (!result)
            {
                return new ErrorDataResult<PersonDto>("Mail Hatas�");

            }
            //return _mapper.Map(person, new PersonDto())/*;*/

            return new SuccessDataResult<PersonDto>(_mapper.Map(person, new PersonDto()));

        }


        public async Task Update(PersonDto personDto)
        {

            var user = await _userRepository.GetAsync(p => p.Id == personDto.UserId);

            user.Username = personDto.Username;
            user.Email = personDto.Email;
            user.UserType = personDto.UserType;
            user.IsApproved = personDto.IsApproved;

            var userUpdate = await _userRepository.UpdateAsync(user);


            await BaseUpdate(personDto);




            #region t�m roller siliniyor
            await AllRoleDelte(user.Id);
            #endregion

            List<RoleUserLine> lineList = new();

            foreach (var item in personDto.RoleDepartments)
            {

                foreach (var roleId in item.RoleIds)
                {
                    var roleLine = new RoleUserLine()
                    {
                        UserId = user.Id,
                        RoleId = new Guid(roleId),
                        Id = Guid.NewGuid(),
                        UnitId = item.UnitId,
                        IsDefaultDepartment = item.IsDefaultDepartment,
                        IsApproved = true
                    };
                    roleLine = BaseEntityHelper.SetBaseEntitiy(roleLine);
                    lineList.Add(roleLine);
                }
            }
            await _roleUserLineRepository.InsertRangeAsync(lineList);
        }



        ///// <summary>
        ///// Ba�vuruya ait fak�lte bazl� sorumlu ��retim eleman� se�mek i�in.
        ///// </summary>
        ///// <param name="facultyId"></param>
        ///// <returns></returns>
        //public async Task<List<NameValue>> GetResponsibleTeacherSelectOptionsByFacultyId(Guid facultyId)
        //{

        //    var listModel = new List<NameValue>();
        //    var entitys = await _repository.GetAll(p => p.User.RoleUserLines.FirstOrDefault(m => m.Units.Id == facultyId).Units == && p.User.UserType == (int)UserType.ResponsibleTeacher);
        //    listModel = await entitys.ProjectTo<NameValue>(_mapper.ConfigurationProvider).ToListAsync();


        //    return listModel;
        //}

        public async Task Delete(Guid userId)
        {

            await AllRoleDelte(userId);


            var person = _repository.Get(p => p.UserId.Equals(userId));
            await _repository.DeleteAsync(person);

            await _userService.Delete(userId);


        }


    }
}


