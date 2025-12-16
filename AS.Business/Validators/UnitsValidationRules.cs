using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class UnitsValidationRules : AbstractValidator<UnitsDto>
    {
        public UnitsValidationRules()
        {

            //Name
            RuleFor(n => n.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim adını giriniz")
                .MaximumLength(400)
                .MinimumLength(10)
                .WithMessage("Lütfen birim adını 10 ile 400 karakter arasında giriniz");

            //Email
            RuleFor(n => n.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim e-mail giriniz")
                .MaximumLength(100)
                .MinimumLength(10)
                .WithMessage("Lütfen birim e-mail'i 10 ile 25 karakter arasında giriniz");

            //Phone
            RuleFor(n => n.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim telefon no giriniz")
                .MaximumLength(400)
                .MinimumLength(10)
                .WithMessage("Lütfen birim telefon no'yu 10 ile 400 karakter arasında giriniz");

            //Url
            RuleFor(n => n.Url)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim url giriniz")
                .MaximumLength(350)
                .MinimumLength(10)
                .WithMessage("Lütfen birim url 10 ile 350 karakter arasında giriniz");

            //Address
            RuleFor(n => n.Address)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim adresi giriniz")
                .MaximumLength(400)
                .MinimumLength(10)
                .WithMessage("Lütfen birim adresi 10 ile 400 karakter arasında giriniz");

            //Fax
            RuleFor(n => n.Fax)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim fax giriniz")
                .MaximumLength(100)
                .MinimumLength(10)
                .WithMessage("Lütfen birim fax 10 ile 100 karakter arasında giriniz");


            //Description
            RuleFor(n => n.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen birim açıklaması giriniz")
                .MaximumLength(700)
                .MinimumLength(10)
                .WithMessage("Lütfen birim açıklamasını 10 ile 700 karakter arasında giriniz");
        }
    }
}
