using AS.Core;
using AS.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace AS.Entities.Entity
{
    public class RoleClient : IdentityRole<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Level { get; set; }

        public Guid Id { get; set; }

        /// <summary>
        /// Oluşturulma zamanı
        /// </summary>
        public DateTime CreationTime { get; set; }


        //public int CreatedById { get; set; }
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public Guid CreatedById { get; set; }


        public virtual User CreatedBy { get; set; }


        /// <summary>
        /// Son değişiklik zamanı
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Son değiştiren kullanıcı
        /// </summary>
        public Guid UpdatedById { get; set; }

        public virtual User UpdatedBy { get; set; }


        /// <summary>
        /// Onaylı kayıt mı?
        /// </summary>
        public bool IsApproved { get; set; }
        public virtual ICollection<RolePermissionLine> RolePermissionLines { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }

    }

}
