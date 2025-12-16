using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Entities.Dtos
{
    public class NewsDto:Dto
    {
        public Guid? ContentId { get; set; }
        public string Title { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? UnitId { get; set; }
        public string Summary { get; set; }
        public string Keyword { get; set; }
        public string ContentDetails { get; set; }
        public string? ExternalUrl { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public NewsType NewsType { get; set; }

        public List<DocumentSummeryDto>? DocumentList { get; set; }
        public List<Guid>? DocumentIdtList { get; set; }

    }
}
