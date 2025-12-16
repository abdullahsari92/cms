using AS.Entities.Dtos;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;


namespace AS.Entities.PublicUI.Dtos.Activity
{
    public class ActivityDtoUI : DtoUI
    {
        public string Title { get; set; }
        public string Speakers { get; set; }
        public string Moderator { get; set; }
        public string Responsible { get; set; }
        public string ContentDetail { get; set; }
        public string PosterUrl { get; set; }
        public Guid LanguageId { get; set; }
        public ActivityCategory ActivityCategory { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Location { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
        public List<DocumentSummeryDto>? DocumentList { get; set; }
        public List<Guid>? DocumentIdtList { get; set; }
    }
}
