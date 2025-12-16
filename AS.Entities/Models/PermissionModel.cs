using AS.Entities.Dtos;
using AS.Entities.Enums;

namespace AS.Entities.Models
{
    public class PermissionModel 
    {
        public PermissionModel()
        {
            ControllerCrudList = new List<PermissionCrudSelected>();
        }

        public bool Checked { get; set; }

        public string ControllerName { get; set; }

        public List<PermissionCrudSelected> ControllerCrudList { get; set; }


    }

}
