using AS.Entities.Dtos;
using FluentValidation;

namespace AS.Business.Validators
{
    public class NewsValidationRules : AbstractValidator<NewsDto>
    {
        public NewsValidationRules()
        {
            //Title
            RuleFor(n => n.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen başlığı giriniz")
                .MaximumLength(400)
                .MinimumLength(10)
                .WithMessage("Lütfen haber başlığını 10 ile 400 karakter arasında giriniz");

            //Summary
            RuleFor(n => n.Summary)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen kısa özeti giriniz")
                .MaximumLength(600)
                .MinimumLength(10)
                .WithMessage("Lütfen kısa özeti 10 ile 400 karakter arasında giriniz");

            //Keyword
            RuleFor(n => n.Keyword)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen keyword giriniz")
                .MaximumLength(150)
                .MinimumLength(10)
                .WithMessage("Lütfen keyword 10 ile 150 karakter arasında giriniz");

            //ContentDetails
            RuleFor(n => n.ContentDetails)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen içerik alanını boş bırakmayınız");

            //NewsType
            RuleFor(n => n.NewsType)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen bir haber tipi seçiniz");

            //PublishBeginDate
            RuleFor(n => n.PublishBeginDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen yayın başlangıç tarihi giriniz");

            //PublishEndDate
            RuleFor(n => n.PublishEndDate)
                .NotEmpty()
                .NotNull()
                 .WithMessage("Lütfen yayın bitiş tarihi giriniz");

            //PublishEndDate - PublishBeginDate 
            RuleFor(n => n.PublishEndDate)
                .GreaterThan(n => n.PublishBeginDate)
                 .WithMessage("Yayın bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

        }
    }
}
