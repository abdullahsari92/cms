using AS.Entities.Base;

namespace AS.Entities.Entity
{
    /// <inheritdoc />
    /// <summary>
    /// Api Resource
    /// </summary>
    public class ActionStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Permission> PermissionLines { get; set; }

    }
}