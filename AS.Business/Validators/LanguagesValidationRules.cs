using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class LanguagesValidationRules : AbstractValidator<LanguageDto>
    {

        public LanguagesValidationRules()
        {
            //Name
            RuleFor(n => n.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen dil adını giriniz")
                .MaximumLength(100)
                .MinimumLength(5)
                .WithMessage("Lütfen dil adını 5 ile 100 karakter arasında giriniz");

        }
    }
}
