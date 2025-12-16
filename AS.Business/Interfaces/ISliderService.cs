using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface ISliderService: IBaseService<Slider,SliderDto>
    {
        Task<IDataResult<SliderDto>> AddSliders(SliderDto sliderDto);
        Task<IDataResult<SliderDto>> UpdateSlider(SliderDto  sliderDto);
        Task<SliderDto> GetById(Guid id);
    }
}
