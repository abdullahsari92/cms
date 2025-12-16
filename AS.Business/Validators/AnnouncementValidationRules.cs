using AS.Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Business.Validators
{
    public class AnnouncementValidationRules : AbstractValidator<AnnouncementDto>
    {
        public AnnouncementValidationRules()
        {
            ////Title
            //RuleFor(n => n.Title)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen başlığı giriniz")
            //    .MaximumLength(400)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen duyuru başlığını 10 ile 400 karakter arasında giriniz");

            ////ContentDetail
            //RuleFor(n => n.ContentDetail)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen içerik alanını boş bırakmayınız")
            //    .MaximumLength(850)
            //    .MinimumLength(10)
            //    .WithMessage("Lütfen duyuru içeriğini 10 ile 850 karakter arasında giriniz");

            ////AnnouncementType
            //RuleFor(n => n.AnnouncementType)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen bir duyuru tipi seçiniz");

            ////PublishBeginDate
            //RuleFor(n => n.PublishBeginDate)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("Lütfen duyuru başlangıç tarihi giriniz");

            ////PublishEndDate
            //RuleFor(n => n.PublishEndDate)
            //    .NotEmpty()
            //    .NotNull()
            //     .WithMessage("Lütfen duyuru bitiş tarihi giriniz");

            ////PublishEndDate - PublishBeginDate 
            //RuleFor(n => n.PublishEndDate)
            //    .GreaterThan(n => n.PublishBeginDate)
            //     .WithMessage("Duyuru bitiş tarihi, başlangıç tarihinden sonra olmalıdır.");
        }
    }
}
