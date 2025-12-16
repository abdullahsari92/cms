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
    public class AnnouncementController : ControllerBase
    {
        private IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var announcementList = await _announcementService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<AnnouncementDto>>(announcementList);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken cancellationToken)
        {

            AnnouncementDto model = await _announcementService.GetById(id);
            return new SuccessDataResult<AnnouncementDto>(model);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByContentId(Guid id, CancellationToken cancellationToken)
        {

            var model = await _announcementService.GetByContentId(id, cancellationToken);
            return new SuccessDataResult<ListModel<AnnouncementDto>>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] AnnouncementDto announcementDto)
        {
            await _announcementService.AddAnnouncements(announcementDto);
            return new SuccessDataResult<AnnouncementDto>();
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] AnnouncementDto announcementDto)
        {

            await _announcementService.UpdateAnnouncement(announcementDto);
            return new SuccessDataResult<AnnouncementDto>();

        }

        //contentId 
        [HttpDelete("{id}/{languageId}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid id, Guid languageId)
        {
            await _announcementService.Delete(id,languageId);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
