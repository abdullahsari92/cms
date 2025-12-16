using AS.Entities.Base;
using AS.Entities.Enums;


namespace AS.Entities.Entity
{
    public class News: BaseEntity
    {
        public string Title { get; set; }
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
        public Guid? LanguageId { get; set; }
        public Language Language { get; set; }
        public string Summary { get; set; }
        public string SeoTitle { get; set; }
        public string Keyword { get; set; }
        public string ContentDetails { get; set; }
        public string ExternalUrl { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public NewsType NewsType { get; set; }
  

    }
}
