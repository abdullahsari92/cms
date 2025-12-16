using AS.Business.Interfaces.PublicUI;
using AS.Core.ValueObjects;
using AS.Entities.PublicUI.Dtos.News;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers.PublicUI
{
    [ApiController]
    [Route("api/public/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "is-Public")]
    public class NewsController : ControllerBase
    {
        private INewsUIService _newsService;

        public NewsController(INewsUIService newsService)
        {
            _newsService = newsService;
        }
        //UnitId
        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(Guid? id, Guid? languageId)
        {
            var newsList = await _newsService.List(id, languageId);
            return new SuccessDataResult<List<NewsListDtoUI>>(newsList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByID(Guid id, CancellationToken cancellationToken)
        {

            NewsDtoUI model = await _newsService.GetById(id);
            return new SuccessDataResult<NewsDtoUI>(model);

        }
    }
}
