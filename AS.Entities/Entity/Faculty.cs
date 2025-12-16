using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class Faculty : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Byte EducationTime { get; set; }
        public virtual ICollection<Department> DepartmentList { get; set; }
    }

}
