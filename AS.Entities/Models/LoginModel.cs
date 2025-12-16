namespace AS.Entities.Models;

public class LoginModel
{

    public string Email { get; set; }

    public string? Password { get; set; }

    public Guid? UnitId { get; set; }

}
