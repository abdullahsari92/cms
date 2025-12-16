using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class SliderController : ControllerBase
    {
        private ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {
            var sliderList = await _sliderService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<SliderDto>>(sliderList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken token)
        {
            SliderDto model = await _sliderService.GetById(id);
            return new SuccessDataResult<SliderDto>(model);
        }


        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] SliderDto sliderDto)
        {
            await _sliderService.AddSliders(sliderDto);
            return new SuccessDataResult<SliderDto>();
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] SliderDto sliderDto)
        {
            await _sliderService.UpdateSlider(sliderDto);
            return new SuccessDataResult<SliderDto>();
        }

        [HttpDelete("{id}")] //Slider'ın dokümanlarını henüz silmiyoruz.
        public async Task<ActionResult<Core.IResult>> Delete(Guid id)
        {
            await _sliderService.BaseDelete(id);
            return Ok(new SuccessResult("İşlem Başarılı"));

        }
    }
}
