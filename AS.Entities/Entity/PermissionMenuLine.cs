using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class PermissionMenuLine : BaseEntity
    {
        public virtual Permission Permission { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
