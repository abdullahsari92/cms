using AS.Business.Interfaces;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class UserManager : BaseManager<User,UserDto>, IUserService
    {
       public readonly IRepository<RoleUserLine> _roleUserLineRepository;

        public UserManager(IRepository<User> repositoryUser, IMapper mapper, IRepository<RoleUserLine> roleUserLine,IRedisService redisService) : base(repositoryUser, mapper, redisService)
        {
            _roleUserLineRepository = roleUserLine;
        }

        public async Task<ListModel<UserDto>> GetAll()
        {
         
            var listModel =  new ListModel<UserDto>();
            var users = await _repository.GetAll();

            var dd = users.ToList();

            listModel.Items = await users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();


                
            return  listModel;
        }
    
        public async Task<UserDto> GetById(Guid id)
        {
            var userQuery = await _repository.GetAll();

            var user = userQuery.Include(p => p.RoleUserLines).FirstOrDefault(p => p.Id == id);

            var userDto = _mapper.Map(user, new UserDto());

            userDto.RoleIds = user.RoleUserLines.Where(p=>!p.IsDeleted).Select(p => p.RoleId.ToString()).ToList();

            return userDto;
        }

        public async Task<List<Role>> GetRoleUser(Guid userId)
        {
            var roleLine = await _roleUserLineRepository.GetAll(x => x.User.Id == userId);

            var roles = await roleLine.Select(m => m.Role).ToListAsync();
            ;
            return roles;
        }

        public async Task  Delete(Guid id)
        {      
           await  _repository.DeleteAsync(id);
        }
 

    }

}
