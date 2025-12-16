using System.ComponentModel.DataAnnotations;

namespace AS.Entities.Models;

public class PasswordChangeModel
{
    //[Required]
    //[EmailAddress]
    //public string Email { get; set; }     

    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
