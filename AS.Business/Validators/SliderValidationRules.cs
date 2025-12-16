using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class SliderValidationRules : AbstractValidator<SliderDto>
    {
        public SliderValidationRules()
        {

            //Title
            RuleFor(n => n.Title)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Lütfen başlığı giriniz")
                    .MaximumLength(250)
                    .MinimumLength(10)
                    .WithMessage("Lütfen slider başlığını 10 ile 250 karakter arasında giriniz");

            //Description
            RuleFor(n => n.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen açıklamayı giriniz")
                .MaximumLength(700)
                .MinimumLength(10)
                .WithMessage("Lütfen slider açıklamasını 10 ile 700 karakter arasında giriniz");

        }
    }
}
