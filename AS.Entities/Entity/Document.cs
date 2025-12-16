using AS.Entities.Base;


namespace AS.Entities.Entity
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; } // Extension (length: 25)
        public string? Keyword { get; set; }
        public string MimeType { get; set; }

        public long DocLength { get; set; } // DocLength


        public ICollection<ContentDocument> ContentDocuments { get; set; }
        public ICollection<Units> UnitsLogoDocumentList { get; set; }
        public ICollection<Units> UnitsLogoTwoDocumentList { get; set; }
        public ICollection<Slider> SliderList { get; set; }

    }

}
