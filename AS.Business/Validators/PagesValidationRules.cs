using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class PagesValidationRules : AbstractValidator<PagesDto>
    {
        public PagesValidationRules()
        {
            ////Title
            //RuleFor(n => n.Title)
            //     .NotEmpty()
            //     .NotNull()
            //     .WithMessage("Lütfen başlık giriniz")
            //     .MaximumLength(400)
            //     .MinimumLength(10)
            //     .WithMessage("Lütfen başlığı 10 ile 400 karakter arasında giriniz");


            ////ContentDetail
            //RuleFor(n => n.ContentDetail)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen içerik alanını boş bırakmayınız");

            ////Keyword
            //RuleFor(n => n.Keyword)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen keyword giriniz")
            //    .MaximumLength(150)
            //    .MinimumLength(5)
            //    .WithMessage("Lütfen keyword 5 ile 150 karakter arasında giriniz");

        }
    }
}
