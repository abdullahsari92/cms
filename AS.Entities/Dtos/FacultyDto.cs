using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;

namespace AS.Entities.Dtos
{
    public class FacultyDto:Dto
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public Byte EducationTime { get; set; }



    }
}
