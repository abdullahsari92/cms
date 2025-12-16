using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Entity;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class LanguageDefinitionController : ControllerBase
    {

        private ILanguageDefinitionService _LanguageManager;
        public LanguageDefinitionController(ILanguageDefinitionService LanguageManager)
        {
            _LanguageManager = LanguageManager;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var LanguageList = await _LanguageManager.GetLLanguageDefination(token);

            return new SuccessDataResult<ListModel<LanguageDefinitionDto>>(LanguageList);

        }


        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] LanguageDefinitionDto languageDto)
        {

            LanguageDefinitionDto model = await _LanguageManager.BaseInsert(languageDto);
            return new SuccessDataResult<LanguageDefinitionDto>(model);
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> AddAll([FromBody] List<LanguageDefinitionDto> LanguageList)
        {

            LanguageDefinitionDto model = new LanguageDefinitionDto();

            var success = _LanguageManager.InsertAll(LanguageList);
            return new SuccessDataResult<bool>(true);

        }


        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] LanguageDefinitionDto LanguageDto)
        {
            var model = await _LanguageManager.BaseUpdate(LanguageDto);
            return new SuccessDataResult<LanguageDefinitionDto>(model);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _LanguageManager.BaseDelete(id);
            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
