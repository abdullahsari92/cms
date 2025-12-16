using AS.Entities.Base;
using AS.Entities.Enums;

namespace AS.Entities.Entity
{
    public class Announcement:BaseEntity
    {
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string ContentDetail { get; set; }
        public int DisplayOrder { get; set; }
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public AnnouncementType AnnouncementType  { get; set; }



    }
}
