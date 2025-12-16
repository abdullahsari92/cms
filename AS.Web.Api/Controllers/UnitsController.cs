using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Web.Api.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class UnitsController : ControllerBase
    {
        private IUnitsService _unitService;
        private IMapper _mapper;
        public UnitsController(IUnitsService unitService, IMapper mapper = null)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {
            var unitDtoList = await _unitService.BaseGetAll(token);
            var unitModelList = _mapper.Map<List<UnitModel>>(unitDtoList.Items); //  burada mapleme yapmazsak çalışmıyor.
            return new SuccessDataResult<ListModel<UnitModel>>(new ListModel<UnitModel> { Items = unitModelList });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id)
        {
            var unit = await _unitService.GetById(id);
            return Ok(new SuccessDataResult<UnitModel>(unit));
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] UnitsDto unitsDto)
        {


            await _unitService.AddUnits(unitsDto);
            return new SuccessDataResult<UnitsDto>();

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] UnitsDto unitsDto)
        {

            await _unitService.BaseUpdate(unitsDto);
            return new SuccessDataResult<UnitsDto>();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid id)
        {
            await _unitService.BaseDelete(id);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
