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
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var activitytList = await _activityService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<ActivityDto>>(activitytList);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken cancellationToken)
        {

            ActivityDto model = await _activityService.GetById(id);
            return new SuccessDataResult<ActivityDto>(model);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByContentId(Guid id, CancellationToken token)
        {
            var model = await _activityService.GetByContentId(id, token);
            return new SuccessDataResult<ListModel<ActivityDto>>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] ActivityDto activityDto)
        {
            await _activityService.AddActivity(activityDto);
            return new SuccessDataResult<ActivityDto>();
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] ActivityDto activityDto)
        {

           await _activityService.UpdateActivity(activityDto);
            return new SuccessDataResult<ActivityDto>();

        }

        //contentId 
        [HttpDelete("{id}/{languageId}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid id, Guid languageId)
        {
            await _activityService.Delete(id, languageId);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
