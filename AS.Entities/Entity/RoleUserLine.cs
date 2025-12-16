using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class RoleUserLine : BaseEntity
    {
        public  Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid? UnitId { get; set; }

        /// <summary>
        ///1 - İlk Kullanıcının default department getireceğiz, 
        ///2 - deparmentId yoksa da bu default da true olacak ve tüm departmentlara yetkisi olmuş olacak.
        /// </summary>
        public bool IsDefaultDepartment { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

        public virtual Units Units   { get; set; }
    }
}
