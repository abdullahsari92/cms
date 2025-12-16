using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Entities.Models
{
    public class RoleDepartmentModel
    {
        public Guid? UnitId { get; set; }
        public Guid? FacultyId { get; set; }

        public string? UnitsName { get; set; }
        public string? FacultyName { get; set; }
        public bool IsDefaultDepartment { get; set; }
        public List<string>? RoleNames { get; set; }

        public List<string>? RoleIds { get; set; }

    }
}

