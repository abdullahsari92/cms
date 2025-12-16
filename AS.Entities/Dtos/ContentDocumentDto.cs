using AS.Entities.Base;
using AS.Entities.Entity;


namespace AS.Entities.Dtos
{
    public class ContentDocumentDto:Dto
    {
        public Guid ContentId { get; set; }
        public Guid DocumentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
