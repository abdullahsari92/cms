using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AutoMapper;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;


namespace AS.Business
{
    public class PagesManager : BaseManager<Pages, PagesDto>, IPagesService
    {
        private readonly IRepository<Content> _repositoryContent;
        private IContentDocumentService _contentDocumentService;
        public PagesManager(IRepository<Pages> repository, IMapper mapper, IRedisService redisService, IRepository<Content> repositoryContent, IContentDocumentService contentDocumentService) : base(repository, mapper, redisService)
        {
            _repositoryContent = repositoryContent;
            _contentDocumentService = contentDocumentService;
        }

        public async Task<IDataResult<PagesDto>> AddPages(PagesDto pagesDto)
        {
            var unitId = UserInfoExtensions.GetUnitId();  
            var pagesData = _mapper.Map<Pages>(pagesDto);

            pagesData.Id = Guid.NewGuid();

            pagesData = BaseEntityHelper.SetBaseEntitiy(pagesData);

            var content = new Content
            {
                Id = Guid.NewGuid(),
                UnitId = unitId.Value,
                ContentCategory = ContentCategory.Pages,  
                PagesList = new List<Pages>() 
            };

 
            content = BaseEntityHelper.SetBaseEntitiy(content);
            pagesData.ContentId = content.Id;
            pagesData.Content = content;
            await _repository.InsertAsync(pagesData);

            var contentId = pagesData.ContentId;
            await _contentDocumentService.ProcessDocumentListAsync(pagesDto.DocumentList, contentId, DocumentType.Pages);
            await _contentDocumentService.ProcessDocumentIdtListAsync(pagesDto.DocumentIdtList, contentId);

            return new SuccessDataResult<PagesDto>(pagesDto, "Sayfa başarıyla eklendi.");
        }
        public async Task<PagesDto> GetById(Guid id)
        {

            var query = await _repository.GetAll(a => a.Id == id);
            var pages = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var pagesDto = _mapper.Map<PagesDto>(pages);
            pagesDto.DocumentList = pages?.Content?.ContentDocuments
                                            .Where(d => d.Document != null)
                                           .Select(d => new DocumentSummeryDto
                                           {
                                               Path = d.Document.Path.ToFullUrl(),
                                               Description = d.Document.Description,
                                               Name = d.Document.Name,
                                               Id = d.DocumentId
                                           })
                                           .ToList() ?? new List<DocumentSummeryDto>();
            return pagesDto;

        }
    }
}
