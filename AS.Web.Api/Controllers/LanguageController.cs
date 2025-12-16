using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Simple;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class LanguageController : ControllerBase
    { 
        private ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

   

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var languageList = await _languageService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<LanguageDto>>(languageList);

        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {
            var languageList = await _languageService.GetSelectOptionsLanguage();

            return new SuccessDataResult<List<NameValue>>(languageList);
        }
        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] LanguageDto languageDto)
        {

            var result = await _languageService.BaseInsert(languageDto);
            return new SuccessDataResult<LanguageDto>(result);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] LanguageDto languageDto)
        {

            var result = await _languageService.BaseUpdate(languageDto);
            return new SuccessDataResult<LanguageDto>(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid Id)
        {
            await _languageService.BaseDelete(Id);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
