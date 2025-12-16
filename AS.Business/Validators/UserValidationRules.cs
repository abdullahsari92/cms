using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class UserValidationRules : AbstractValidator<UserDto>
    {
        public UserValidationRules()
        {
            
            //Username
            RuleFor(n => n.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen kullanıcı adını giriniz")
                .MaximumLength(512)
                .MinimumLength(10)
                .WithMessage("Lütfen kullanıcı adını 10 ile 512 karakter arasında giriniz");

            //Password
            RuleFor(n => n.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen şifre giriniz")
                .MaximumLength(128)
                .MinimumLength(5)
                .WithMessage("Lütfen şifreyi 5 ile 128 karakter arasında giriniz");

            //Email
            RuleFor(n => n.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen Email giriniz")
                .MaximumLength(512)
                .MinimumLength(10)
                .WithMessage("Lütfen Emaili 10 ile 512 karakter arasında giriniz");
        
        }
    }
}
