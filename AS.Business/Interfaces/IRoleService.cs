using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Models;
using AS.Entities.Simple;

namespace AS.Business.Interfaces
{
    public interface IRoleService: IBaseService<Role,RoleDto>
    {
        // Task<ListModel<PermissionDto>> Get(Guid roleId);

        Task<ListModel<RoleDto>> GetAll();

        Task<RoleDetailModel> Get(Guid roleId, CancellationToken token);

        Task<List<PermissionModel>> RolePermissionAdd(RoleDetailModel roleAddModel);

        Task<string> GetPermissionClaims(List<Guid> roleIds);

        Task<List<NameValue>> GetSelectOptions();

    }
}
