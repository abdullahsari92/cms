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
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var newsList = await _documentService.GetAll(token);
            return new SuccessDataResult<ListModel<DocumentDto>>(newsList);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid id, CancellationToken cancellationToken)
        {

            DocumentDto model = await _documentService.BaseGetById(id);
            return new SuccessDataResult<DocumentDto>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] DocumentDto documentDto)
        {
            await _documentService.Insert(documentDto);
            return new SuccessDataResult<DocumentDto>();
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] DocumentDto documentDto)
        {

            var result = await _documentService.BaseUpdate(documentDto);
            return new SuccessDataResult<DocumentDto>(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid Id)
        {
            await _documentService.BaseDelete(Id);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
