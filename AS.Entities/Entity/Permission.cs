using AS.Entities.Base;
using AS.Entities.Enums;

namespace AS.Entities.Entity
{
    /// <inheritdoc />
    /// <summary>
    /// Api Resource
    /// </summary>
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public int CRUDActionType { get; set; }
        public virtual ICollection<RolePermissionLine> RolePermissionLines { get; set; }
        public virtual ICollection<PermissionMenuLine> PermissionMenuLines { get; set; }

    }
}