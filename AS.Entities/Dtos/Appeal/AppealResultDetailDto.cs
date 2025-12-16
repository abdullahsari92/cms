using AS.Entities.Base;
using AS.Entities.Entity;

namespace AS.Entities.Dtos
{
    public class AppealResultDetailDto :Dto
    {

        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string IdentityNo { get; set; } // tckimlik
        public string StudentPhone { get; set; }
        public Guid StudentId { get; set; }

        public string DepartmentName { get; set; }
        public string? CompanyName { get; set; }
        public string? ResponsibleTeacher { get; set; }




    }
}
