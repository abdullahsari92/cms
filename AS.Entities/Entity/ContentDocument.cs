using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class ContentDocument:BaseEntity
    {
        public Guid  ContentId { get; set; }
        public Content Content { get; set; }

        public Guid DocumentId { get; set; }
        public Document Document { get; set; }


    }
}
