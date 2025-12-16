namespace AS.Entities.Models;

public class AuthModel
{

    public string Claims { get; set; }

    public string Token { get; set; }
    public string Email { get; set; }

    public string FullName { get; set; }

    public string RoleName { get; set; }

    public int UserType { get; set; }

    public Guid? StudentId { get; set; }

    public Guid? UnitId { get; set; }
    public string DeparmentName { get; set; }
    public string FacultyName { get; set; }




}
