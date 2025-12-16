
using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Activity;
using AS.Entities.PublicUI.Dtos.News;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business.PublicUIManager
{
    public class NewsUIManager : BaseUIManager<News, NewsDtoUI>, INewsUIService
    {
        public NewsUIManager(IRepository<News> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }

        public async Task<List<NewsListDtoUI>> List(Guid? UnitId, Guid? languageId)
        {
            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;

          
            var defaultUnitId = new Guid("e0f90674-f6b6-4370-8f0e-1fb93124d115"); 
            var unitId = UnitId ?? defaultUnitId;

            var query = await _repository.GetAll(x => x.LanguageId == langId
                                                      && x.Content.UnitId != null
                                                      && x.Content.UnitId == unitId
            );

            var newsDtoList = await query.Include(a => a.Content)
                                  .ThenInclude(c => c.ContentDocuments)
                                  .ThenInclude(d => d.Document).Select(d => new NewsListDtoUI
                                  {
                                      Id = d.Id,
                                      Title= d.Title,
                                      Summary =d.Summary,
                                      NewsType= d.NewsType,
                                      CoverImage = new DocumentSummeryDto
                                      {
                                          Path = d.Content.ContentDocuments.FirstOrDefault().Document.Path,
                                          Description   =d.Content.ContentDocuments.FirstOrDefault().Document.Description,
                                          Name = d.Content.ContentDocuments.FirstOrDefault().Document.Name,
                                      }
                                  }).ToListAsync();

           return newsDtoList;
        }

        public async Task<NewsDtoUI> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var activity = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var newsDto = _mapper.Map<NewsDtoUI>(activity);
            newsDto.DocumentList = activity?.Content?.ContentDocuments
                                                .Where(d => d.Document != null)
                                               .Select(d => new DocumentSummeryDto
                                               {
                                                   Path = d.Document.Path.ToFullUrl(),
                                                   Description = d.Document.Description,
                                                   Name = d.Document.Name,
                                                   Id = d.Document.Id
                                               })
                                               .ToList() ?? new List<DocumentSummeryDto>();

            return newsDto;
        }
    }
}
