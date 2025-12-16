using AS.Entities.Dtos;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;

namespace AS.Entities.PublicUI.Dtos.News
{
    public class NewsDtoUI:DtoUI
    {
        public Guid? ContentId { get; set; }
        public string Title { get; set; }
        public Guid? LanguageId { get; set; }
        public string Summary { get; set; }
        public string Keyword { get; set; }
        public string ContentDetail { get; set; }
        public string? ExternalUrl { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public NewsType NewsType { get; set; }

        public List<DocumentSummeryDto>? DocumentList { get; set; }
        public List<Guid>? DocumentIdtList { get; set; }
    }
}
