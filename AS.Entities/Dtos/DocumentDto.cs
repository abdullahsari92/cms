using AS.Entities.Base;

namespace AS.Entities.Dtos
{
    public class DocumentDto : Dto
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public string Path { get; set; }

        public string DocumentType { get; set; }

        public string Extension { get; set; } // Extension (length: 25)
        public string Keyword { get; set; }
        public string MimeType { get; set; }

        public Guid? UnitId { get; set; }


        public long DocLength { get; set; } // DocLength

        public string Base64 { get; set; }


    }
}
