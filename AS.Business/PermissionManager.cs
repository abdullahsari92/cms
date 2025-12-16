using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using Business.Adapters.Redis;

namespace AS.Business
{
    public class PermissionManager : BaseManager<Permission, PermissionDto>, IPermissionService
    {

        private readonly IRepository<RoleUserLine> _repositoryRoleUser;
    

        public PermissionManager(  IMapper mapper, IRepository<Permission> repositoryPermission, IRedisService redisService) :base(repositoryPermission,mapper, redisService)
        {
         
        }


        public async Task<PermissionDto> Insert(PermissionDto permissionDto)
        {

            try
            {

                var isEntity =  _repository.IsExist(p => p.ControllerName == permissionDto.ControllerName && p.ActionName == permissionDto.ActionName);
                if(!isEntity)
                {
                    return await this.BaseInsert(permissionDto);
                
                }

               return null;

            }
            catch (Exception ex)
            {

                
             var mesaj = ex.Message;
                return null;
            }
        }
       

    }

}
