using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Dtos.Activity;
using AS.Entities.PublicUI.Dtos.Announcement;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;


namespace AS.Business
{
    public class NewsManager : BaseManager<News, NewsDto>, INewsService
    {
        private readonly IRepository<Content> _repositoryContent;
        private readonly IContentDocumentService _contentDocumentService;

        public NewsManager(IRepository<News> repository, IMapper mapper, IRedisService redisService, IRepository<Content> repositoryContent, IContentDocumentService contentDocumentService) : base(repository, mapper, redisService)
        {
            _repositoryContent = repositoryContent;
            _contentDocumentService = contentDocumentService;
        }


        public async Task<IDataResult<NewsDto>> AddNews(NewsDto newsDto)
        {
            var unitId = newsDto.UnitId ?? UserInfoExtensions.GetUnitId();


            var isExist = await IsNewsExistInSameLanguageAsync(newsDto);
            if (isExist)
            {
                throw new DuplicateException("Bu dilde bu içerik zaten mevcut.");
            }

            Content content = null;
            News newsData = _mapper.Map<News>(newsDto);
            newsData.Id = Guid.NewGuid();
            newsData.SeoTitle = newsData.Title.ToStringForSeo();
            newsData = BaseEntityHelper.SetBaseEntitiy(newsData);

            //ContentId geldiği durumlarda.
            if (newsDto.ContentId.HasValue)
            {
                var contentQuery = await _repositoryContent.GetAll(p => p.Id == newsDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                newsData.ContentId = newsDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.News,
                    NewsList = new List<News>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                newsData.Content = content;
            }

            await _repository.InsertAsync(newsData);
            var contentId = newsData?.ContentId ?? content.Id;
            await _contentDocumentService.ProcessDocumentListAsync(newsDto.DocumentList, contentId, DocumentType.News);
            await _contentDocumentService.ProcessDocumentIdtListAsync(newsDto.DocumentIdtList, contentId);

            var message = newsDto.ContentId.HasValue ? "Haber başarıyla eklendi." : "Haber ve içerik başarıyla eklendi.";
            return new SuccessDataResult<NewsDto>(newsDto, message);

        }
        public async Task<IDataResult<NewsDto>> UpdateNews(NewsDto newsDto)
        {
            var unitId = newsDto.UnitId ?? UserInfoExtensions.GetUnitId();

            var existingNewsQuery = await _repository.GetAll(a => a.Id == newsDto.Id);
            var existingNews = await existingNewsQuery.FirstOrDefaultAsync();
            if (existingNews == null)
            {
                throw new NotFoundException("Güncellenmek istenen Duyuru bulunamadı.");
            }

            _mapper.Map(newsDto, existingNews);
            existingNews.SeoTitle = existingNews.Title.ToStringForSeo();
            existingNews = BaseEntityHelper.SetBaseEntitiy(existingNews);
            Content content = null;

            if (newsDto.ContentId.HasValue)
            {
                var contentQuery = await _repositoryContent.GetAll(p => p.Id == newsDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                existingNews.ContentId = newsDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.News,
                    NewsList = new List<News>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                existingNews.Content = content;
            }


            await _repository.UpdateAsync(existingNews);
            var contentId = existingNews?.ContentId ?? content.Id;


            await _contentDocumentService.UpdateDocumentListAsync(newsDto.DocumentList, contentId, DocumentType.News);
            await _contentDocumentService.UpdateDocumentIdtListAsync(newsDto.DocumentIdtList, contentId);

            return new SuccessDataResult<NewsDto>(newsDto, "Haber başarıyla güncellendi.");
        }
        public async Task Delete(Guid contentId, Guid languageId)
        {
            var announcementsList = (await _repository.GetAll(a => a.ContentId == contentId)).ToList();

            if (!announcementsList.Any())
            {
                return;
            }

            var languageSpecificAnnouncements = announcementsList
                .Where(a => a.LanguageId == languageId)
                .ToList();

            var uniqueLanguagesCount = announcementsList.Select(a => a.LanguageId).Distinct().Count();

            if (uniqueLanguagesCount > 1)
            {
                foreach (var announcement in languageSpecificAnnouncements)
                {
                    await _repository.DeleteAsync(announcement);
                }
            }
            else
            {
                foreach (var announcement in announcementsList)
                {
                    await _repository.DeleteAsync(announcement);
                }

                var content = (await _repositoryContent.GetAll(c => c.Id == contentId)).FirstOrDefault();
                if (content != null)
                {
                    await _repositoryContent.DeleteAsync(content);
                }
            }
        }
        public async Task<NewsDto> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var news = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var newsDto = _mapper.Map<NewsDto>(news);
            newsDto.DocumentList = news?.Content?.ContentDocuments
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
        public async Task<ListModel<NewsDto>> GetByContentId(Guid ContentId, CancellationToken token)
        {
            var listModel = new ListModel<NewsDto>();
            var news = await _repository.GetAll(p => p.ContentId == ContentId);

            listModel.Items = await news.ProjectTo<NewsDto>(_mapper.ConfigurationProvider).ToListAsync(token);

            return listModel;

        }

        #region Helper Methods
        private async Task<bool> IsNewsExistInSameLanguageAsync(NewsDto newsDto)
        {
            var isExistQuery = await _repository.GetAll(a => a.LanguageId == newsDto.LanguageId);

            if (newsDto.ContentId.HasValue)
            {
                isExistQuery = isExistQuery.Where(a => a.ContentId == newsDto.ContentId);
            }
            else
            {
                isExistQuery = isExistQuery.Where(a => a.Title == newsDto.Title);
            }

            return await isExistQuery.AnyAsync();
        }
        #endregion
    }
}
