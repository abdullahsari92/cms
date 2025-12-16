
using AS.Entities.Base;


namespace AS.Entities.Entity
{
    public class Pages:BaseEntity
    {
        public string Title { get; set; }
        public Guid ContentId { get; set; }
        public Content  Content { get; set; }
        public Guid? MenuId { get; set; }
        public Menu Menu { get; set; }
        public Guid LanguageId { get; set; }
        public Language  Language { get; set; }
        public string SeoTitle { get; set; }
        public string ContentDetail { get; set; }
        public string Keyword { get; set; }
        public string ExternalUrl { get; set; }


    }
}
