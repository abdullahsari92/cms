using AS.Entities.Dtos;
using FluentValidation;


namespace AS.Business.Validators
{
    public class ActivityValidationRules : AbstractValidator<ActivityDto>
    {
        public ActivityValidationRules()
        {
            //Title
            //RuleFor(n => n.Title)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen başlığı giriniz")
            //    .MaximumLength(400)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik başlığını 10 ile 400 karakter arasında giriniz");

            //Speakers
            //RuleFor(n => n.Speakers)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen konuşmacıları giriniz")
            //    .MaximumLength(250)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik başlığını 10 ile 250 karakter arasında giriniz");

            ////Moderator
            //RuleFor(n => n.Moderator)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen moderatörleri giriniz")
            //    .MaximumLength(250)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik moderatörlerini 10 ile 250 karakter arasında giriniz");

            ////Responsible
            //RuleFor(n => n.Responsible)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen sorumluları giriniz")
            //    .MaximumLength(250)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik sorumlularını 10 ile 250 karakter arasında giriniz");

            ////ContentDetail
            //RuleFor(n => n.ContentDetail)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen içerik alanını boş bırakmayınız")
            //    .MaximumLength(850)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik içerik alanını 10 ile 850 karakter arasında giriniz");

            ////PosterUrl
            //RuleFor(n => n.PosterUrl)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen poster url giriniz")
            //    .MaximumLength(250)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen etkinlik poster url 10 ile 250 karakter arasında giriniz");

            //ActivityCategory
            //RuleFor(n => n.ActivityCategory)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen bir etkinlik kategorisi seçiniz");

            ////ActivityType
            //RuleFor(n => n.ActivityType)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen bir etkinlik tipi seçiniz");

            //PublishBeginDate
            //RuleFor(n => n.PublishBeginDate)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen etkinlik sitede yayın başlangıç tarihi giriniz");

            ////PublishEndDate
            //RuleFor(n => n.PublishEndDate)
            //    .NotEmpty()
            //    .NotNull()
            //     .WithMessage("Lütfen etkinlik sitede yayın bitiş tarihi giriniz");

            ////PublishEndDate - PublishBeginDate 
            //RuleFor(n => n.PublishEndDate)
            //    .GreaterThan(n => n.PublishBeginDate)
            //     .WithMessage("Etkinlik sitede yayın bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

            ////ActivityStartDate
            //RuleFor(n => n.PublishBeginDate)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen etkinliğin gerçekleşme başlangıç tarihi giriniz");

            ////ActivityEndDate
            //RuleFor(n => n.PublishEndDate)
            //    .NotEmpty()
            //    .NotNull()
            //     .WithMessage("Lütfen etkinliğin gerkçekleşme bitiş tarihi giriniz");

            ////ActivityStartDate - ActivityEndDate 
            //RuleFor(n => n.PublishEndDate)
            //    .GreaterThan(n => n.PublishBeginDate)
            //     .WithMessage("Etkinliğin gerçekleşme bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");

        }
    }
}
