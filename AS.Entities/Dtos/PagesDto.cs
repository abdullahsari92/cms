using AS.Entities.Base;


namespace AS.Entities.Dtos
{
    public class PagesDto : Dto
    {
        public string Title { get; set; }
        public Guid? MenuId { get; set; }
        public Guid? LanguageId { get; set; }
        public string SeoTitle { get; set; }
        public string ContentDetail { get; set; }
        public string Keyword { get; set; }
        public string ExternalUrl { get; set; }
        public List<DocumentSummeryDto>? DocumentList { get; set; }
        public List<Guid>? DocumentIdtList { get; set; }
    }
}
