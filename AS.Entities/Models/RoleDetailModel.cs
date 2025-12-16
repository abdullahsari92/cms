using AS.Entities.Dtos;
namespace AS.Entities.Models
{
    public class RoleDetailModel 
    {
        public List<PermissionModel> PermissionList { get; set; }

        public RoleDto RoleDto { get; set; }


    }

}
