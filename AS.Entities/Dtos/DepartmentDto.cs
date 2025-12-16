using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;

namespace AS.Entities.Dtos
{
    public class DepartmentDto:Dto
    {

        public string Name { get; set; }
        public string Code { get; set; }

        public string FacultyName { get; set; }

        public Guid FacultyId { get; set; }

    }
}
