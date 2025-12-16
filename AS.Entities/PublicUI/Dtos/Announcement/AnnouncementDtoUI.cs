using AS.Entities.Dtos;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;
namespace AS.Entities.PublicUI.Dtos.Announcement
{
    public class AnnouncementDtoUI : DtoUI
    {
        public Guid? ContentId { get; set; }
        public string Title { get; set; }
        public string ContentDetail { get; set; }
        public int DisplayOrder { get; set; }
        public Guid LanguageId { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
        public List<DocumentSummeryDto>? DocumentList { get; set; }
        public List<Guid>? DocumentIdtList { get; set; }
    }
}
