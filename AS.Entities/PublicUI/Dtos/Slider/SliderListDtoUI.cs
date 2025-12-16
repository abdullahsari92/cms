using AS.Entities.Dtos;
using AS.Entities.PublicUI.Base;

namespace AS.Entities.PublicUI.Dtos.Slider
{
    public class SliderListDtoUI:DtoUI
    {
        public string Title { get; set; }
        public Guid? LanguageId { get; set; }
        public DocumentSummeryDto DocumentSummeryDto { get; set; }

    }
}
