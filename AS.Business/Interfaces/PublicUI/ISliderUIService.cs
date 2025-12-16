using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Slider;
namespace AS.Business.Interfaces.PublicUI
{
    public interface ISliderUIService:IBaseUIService<Slider,SliderDtoUI>
    {
        Task<List<SliderListDtoUI>> List(Guid? UnitId,Guid? languageId);
        Task<SliderDtoUI> GetById(Guid id);
    }
}
