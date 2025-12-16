using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class ConfigurationController : ControllerBase
    {

        private ISettingService _settingService;
        private ILanguageDefinitionService _LanguageManager;

        public ConfigurationController(ISettingService settingService, ILanguageDefinitionService languageManager)
        {
            _settingService = settingService;
            _LanguageManager = languageManager;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> LanguageDefinition(CancellationToken token)
        {

            var LanguageList = await _LanguageManager.GetLLanguageDefination(token);
             return new SuccessDataResult<ListModel<LanguageDefinitionDto>>(LanguageList);
        }


        [HttpGet("{code}")]
        public async Task<ActionResult<Core.IResult>> GetByCode(string code)
        {           
                var model = await _settingService.GetByCode(code);

                return new SuccessDataResult<SettingDto>(model); 
        }


    }
}
