using AS.Business.Interfaces.PublicUI;
using AS.Core.ValueObjects;
using AS.Entities.PublicUI.Dtos.Slider;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers.PublicUI
{

    [ApiController]
    [Route("api/public/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "is-Public")]
    public class SliderController : ControllerBase
    {
        private ISliderUIService _sliderService;

        public SliderController(ISliderUIService sliderService)
        {
            _sliderService = sliderService;
        }
        //UnitId
        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(Guid? id, Guid? languageId)
        {
            var sliderList = await _sliderService.List(id, languageId);
            return new SuccessDataResult<List<SliderListDtoUI>>(sliderList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByID(Guid id, CancellationToken cancellationToken)
        {

            SliderDtoUI model = await _sliderService.GetById(id);
            return new SuccessDataResult<SliderDtoUI>(model);

        }
    }
}
