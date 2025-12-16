using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class PagesController : ControllerBase
    {
       private  IPagesService _pagesService;

        public PagesController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var personList = await _pagesService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<PagesDto>>(personList);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] PagesDto pagesDto)
        {

           await _pagesService.AddPages(pagesDto);
            return new SuccessDataResult<PagesDto>();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken cancellationToken)
        {

            PagesDto model = await _pagesService.BaseGetById(id);
            return new SuccessDataResult<PagesDto>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] PagesDto pagesDto)
        {

            var result = await _pagesService.BaseUpdate(pagesDto);
            return new SuccessDataResult<PagesDto>(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid Id)
        {



            await _pagesService.BaseDelete(Id);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }

    }
}
