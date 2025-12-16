using AS.Business.Interfaces.PublicUI;
using AS.Core.ValueObjects;
using AS.Entities.PublicUI.Dtos.Announcement;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers.PublicUI
{
    [ApiController]
    [Route("api/public/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "is-Public")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementUIService _announcementUIService;

        public AnnouncementController(IAnnouncementUIService announcementUIService)
        {
            _announcementUIService = announcementUIService;
        }
   
        [HttpGet]     //UnitId
        public async Task<ActionResult<Core.IResult>> List(Guid? id, Guid? languageId)
        {

            var announcementList = await _announcementUIService.List(id, languageId);
            return new SuccessDataResult<List<AnnouncementListDtoUI>>(announcementList);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByID(Guid id, CancellationToken cancellationToken)
        {

            AnnouncementDtoUI model = await _announcementUIService.GetById(id);
            return new SuccessDataResult<AnnouncementDtoUI>(model);

        }
    }
}
