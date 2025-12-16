using AS.Entities.Base;
using AS.Entities.Enums;

namespace AS.Entities.Entity
{
    public class Activity : BaseEntity
    {
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string Speakers { get; set; }
        public string Moderator { get; set; }
        public string Responsible { get; set; }
        public string ContentDetail { get; set; }
        public string PosterUrl { get; set; }
        public ActivityCategory ActivityCategory { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Location { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }

    }
}
