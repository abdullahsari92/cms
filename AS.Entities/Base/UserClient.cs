using AS.Core;
using AS.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace AS.Entities.Entity
{
    public class UserClient : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => FirstName + " " + LastName;
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

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


        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }

        public virtual ICollection<User> UsersCreatedBy { get; set; }

        public virtual ICollection<User> UsersUpdatedBy { get; set; }

        public virtual ICollection<Role> RolesCreatedBy { get; set; }

        public virtual ICollection<Menu> MenusUpdatedBy { get; set; }

        public virtual ICollection<Menu> MenusCreatedBy { get; set; }

        public virtual ICollection<Role> RolesUpdatedBy { get; set; }

        public virtual ICollection<LanguageDefinition> LanguageCreatedBy { get; set; }
        public virtual ICollection<LanguageDefinition> LanguageUpdatedBy { get; set; }

        public virtual ICollection<Permission> PermissionsCreatedBy { get; set; }
        public virtual ICollection<Permission> PermissionsUpdatedBy{ get; set; }

        public virtual ICollection<RolePermissionLine> RolePermissionLinesCreatedBy { get; set; }
        public virtual ICollection<RolePermissionLine> RolePermissionLinesUpdatedBy { get; set; }

        public virtual ICollection<RoleUserLine> RoleUserLinesCreatedBy { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLinesUpdatedBy { get; set; }

        public virtual ICollection<PermissionMenuLine> PermissionMenuLinesCreatedBy { get; set; }
        public virtual ICollection<PermissionMenuLine> PermissionMenuLinesUpdatedBy { get; set; }





    }
}
