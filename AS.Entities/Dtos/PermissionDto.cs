using AS.Entities.Base;
using AS.Entities.Enums;

namespace AS.Entities.Dtos
{
    public class PermissionDto:Dto
    {
                   
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool Checked { get; set; }

        public int CRUDActionType { get; set; }


    }
}
