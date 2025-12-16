using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Models;

namespace AS.Entities.Dtos
{
    public class PersonDto:Dto
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string? IdentityNo { get; set; } // tckimlik
        public string? Phone { get; set; }
        public Guid? UnitId { get; set; } // zorunlu değil
        public string? UnitsName { get; set; } // zorunlu değil

        public Guid UserId { get; set; }

        public int UserType { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }

        public string? RoleName { get; set; }
        public List<string>? RoleIds { get; set; }
        public List<RoleDepartmentModel>? RoleDepartments { get; set; }



    }

    
}
