using AS.Entities.Base;
namespace AS.Entities.Entity
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public Guid LanguageId { get; set; }
        public Guid UnitId { get; set; }
        public Guid DocumentId { get; set; }

        public Document Document { get; set; }
        public Units Units { get; set; }
        public Language Language { get; set; }

    }
}
