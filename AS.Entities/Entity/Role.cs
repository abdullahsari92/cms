using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public string Code { get; set; }

        public virtual ICollection<RolePermissionLine> RolePermissionLines { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }

    }

}
