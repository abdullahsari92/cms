using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Simple;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [Route("api/public/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Public")]
    public class WelcomController : ControllerBase
    { 
        private ILanguageService _languageService;
        private IUnitsService _unitsService;

        public WelcomController(ILanguageService languageService, IUnitsService unitsService)
        {
            _languageService = languageService;
            _unitsService = unitsService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> GetLanguage()
        {

            var languageList = await _languageService.BaseGetSelectOptions();
            return new SuccessDataResult<List<NameValue>>(languageList);

        }
        /// <summary>
        /// Başvurusu olan tek bir öğrencinin detaylı bilgisini dönüyor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{unitUrl}")]
        public async Task<ActionResult<Core.IResult>> GetUnitByUrl(string unitUrl, CancellationToken token)
        {
            var UnitModel = await _unitsService.GetUnitByUrl(unitUrl, token);
            return Ok(new SuccessDataResult<UnitModel>(UnitModel));
        }

    }
}
