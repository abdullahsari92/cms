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
    public class LogInfoController : ControllerBase
    {
        private ILogInfoService _logInfoService;

        public LogInfoController(ILogInfoService logInfoService)
        {
            _logInfoService = logInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List()
        {
            var logInfoList = await _logInfoService.GetAllLogInfoAsync();

            return new SuccessDataResult<List<LogInfoDto>>(logInfoList);

        }
    }
}
