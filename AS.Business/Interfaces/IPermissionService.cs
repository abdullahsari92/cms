using System.Linq.Expressions;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface IPermissionService: IBaseService<Permission,PermissionDto>
    {


        Task<PermissionDto> Insert(PermissionDto permissionDto);

    }
}
