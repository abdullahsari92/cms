using AS.Entities.Base;


namespace AS.Entities.Dtos
{
    public class SliderDto:Dto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public Guid LanguageId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid DocumentId { get; set; }
        public DocumentSummeryDto DocumentSummeryDto { get; set; }
    }
}
