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

    public class NewsController : ControllerBase
    {
        private INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var newsList = await _newsService.BaseGetAll(token);
            return new SuccessDataResult<ListModel<NewsDto>>(newsList);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken cancellationToken)
        {

            NewsDto model = await _newsService.GetById(id);
            return new SuccessDataResult<NewsDto>(model);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetByContentId(Guid id, CancellationToken cancellationToken)
        {

            var model = await _newsService.GetByContentId(id, cancellationToken);
            return new SuccessDataResult<ListModel<NewsDto>>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] NewsDto newsDto)
        {

            await _newsService.AddNews(newsDto);
            return new SuccessDataResult<NewsDto>();
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] NewsDto newsDto)
        {

            await _newsService.UpdateNews(newsDto);
            return new SuccessDataResult<NewsDto>();

        }

        //contentId 
        [HttpDelete("{id}/{languageId}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid id, Guid languageId)
        {
            await _newsService.Delete(id, languageId);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
