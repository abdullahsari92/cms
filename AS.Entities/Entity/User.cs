using AS.Entities.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AS.Entities.Entity
{
    public class User : BaseEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }  
        public int UserType { get; set; }
        public Person? Person { get; set; }

        public virtual ICollection<RoleUserLine> RoleUserLines { get; set; }

        public virtual ICollection<User> UsersCreatedBy { get; set; }

        public virtual ICollection<User> UsersUpdatedBy { get; set; }

        public virtual ICollection<Role> RolesCreatedBy { get; set; }

        public virtual ICollection<History> HistoryTransactionerUser { get; set; }

        public virtual ICollection<Document> DocumentsCreatedBy { get; set; }
        public virtual ICollection<Document> DocumentsUpdatedBy { get; set; }
         
        public virtual ICollection<Faculty> FacultiesCreatedBy { get; set; }
        public virtual ICollection<Faculty> FacultiesUpdatedBy { get; set; }

        public virtual ICollection<Department> DepartmentsCreatedBy { get; set; }
        public virtual ICollection<Department> DepartmentsUpdatedBy { get; set; }

        public virtual ICollection<Person> PersonsCreatedBy { get; set; }
        public virtual ICollection<Person> PersonsUpdatedBy { get; set; }

        public virtual ICollection<Menu> MenusUpdatedBy { get; set; }

        public virtual ICollection<Menu> MenusCreatedBy { get; set; }

        public virtual ICollection<Role> RolesUpdatedBy { get; set; }

        public virtual ICollection<LanguageDefinition> LanguageDefinitionCreatedBy { get; set; }
        public virtual ICollection<LanguageDefinition> LanguageDefinitionUpdatedBy { get; set; }

        public virtual ICollection<Permission> PermissionsCreatedBy { get; set; }
        public virtual ICollection<Permission> PermissionsUpdatedBy{ get; set; }

        public virtual ICollection<Setting> SettingCreatedBy { get; set; }
        public virtual ICollection<Setting> SettingUpdatedBy { get; set; }



        public virtual ICollection<RolePermissionLine> RolePermissionLinesCreatedBy { get; set; }
        public virtual ICollection<RolePermissionLine> RolePermissionLinesUpdatedBy { get; set; }

        public virtual ICollection<RoleUserLine> RoleUserLinesCreatedBy { get; set; }
        public virtual ICollection<RoleUserLine> RoleUserLinesUpdatedBy { get; set; }

        public virtual ICollection<PermissionMenuLine> PermissionMenuLinesCreatedBy { get; set; }
        public virtual ICollection<PermissionMenuLine> PermissionMenuLinesUpdatedBy { get; set; }

        public virtual ICollection<Content> ContentCreatedBy { get; set; }
        public virtual ICollection<Content> ContentUpdatedBy { get; set; }


        public virtual ICollection<News> NewsCreatedBy { get; set; }
        public virtual ICollection<News> NewsUpdatedBy { get; set; }


        public virtual ICollection<Units> UnitsCreatedBy { get; set; }
        public virtual ICollection<Units> UnitsUpdatedBy { get; set; }

        public virtual ICollection<Pages> PagesCreatedBy { get; set; }
        public virtual ICollection<Pages> PagesUpdatedBy { get; set; }
        public virtual ICollection<Language> LanguageCreatedBy { get; set; }
        public virtual ICollection<Language> LanguageUpdatedBy { get; set; }  
        public virtual ICollection<ContentDocument> ContentDocumentCreatedBy { get; set; }
        public virtual ICollection<ContentDocument> ContentDocumentUpdatedBy { get; set; }
        public virtual ICollection<Announcement> AnnouncementCreatedBy { get; set; }
        public virtual ICollection<Announcement> AnnouncementUpdatedBy { get; set; }
        public virtual ICollection<Activity> ActivityCreatedBy { get; set; }
        public virtual ICollection<Activity> ActivityUpdatedBy { get; set; }
        public virtual ICollection<Slider> SliderCreatedBy { get; set; }
        public virtual ICollection<Slider> SliderUpdatedBy { get; set; }
    }
}
