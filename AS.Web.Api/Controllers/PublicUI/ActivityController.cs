using AS.Business.Interfaces.PublicUI;
using AS.Core.ValueObjects;
using AS.Entities.PublicUI.Dtos.Activity;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers.PublicUI
{

    [ApiController]
    [Route("api/public/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "is-Public")]

    public class ActivityController : ControllerBase
    {
        private readonly IActivityUIService _activityUIService;

        public ActivityController(IActivityUIService activityUIService)
        {
            _activityUIService = activityUIService;
        }
       
        [HttpGet] //UnitId 
        public async Task<ActionResult<Core.IResult>> List(Guid? id, Guid? languageId)
        {
            var activityList = await _activityUIService.List(id, languageId);
            return new SuccessDataResult<List<ActivityListDtoUI>>(activityList);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByID(Guid id, CancellationToken cancellationToken)
        {

            ActivityDtoUI model = await _activityUIService.GetById(id);
            return new SuccessDataResult<ActivityDtoUI>(model);

        }
    }
}
