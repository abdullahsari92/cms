using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Dtos.Announcement;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class ActivityManager : BaseManager<Activity, ActivityDto>, IActivityService
    {
        private readonly IRepository<Content> _contentRepository;
        private IContentDocumentService _contentDocumentService;
        public ActivityManager(IRepository<Activity> repository, IMapper mapper, IRedisService redisService, IRepository<Content> contentRepository, IContentDocumentService contentDocumentService = null) : base(repository, mapper, redisService)
        {
            _contentRepository = contentRepository;
            _contentDocumentService = contentDocumentService;
        }

        public async Task<IDataResult<ActivityDto>> AddActivity(ActivityDto activityDto)
        {
            var unitId = activityDto.UnitId ?? UserInfoExtensions.GetUnitId();


            var isExist = await IsActivityExistInSameLanguageAsync(activityDto);
            if (isExist)
            {
                throw new DuplicateException("Bu dilde bu içerik zaten mevcut.");
            }

            Content content = null;
            Activity activityData = _mapper.Map<Activity>(activityDto);
            activityData.Id = Guid.NewGuid();
            activityData.SeoTitle = activityData.Title.ToStringForSeo();
            activityData = BaseEntityHelper.SetBaseEntitiy(activityData);

            if (activityDto.ContentId.HasValue)
            {
                var contentQuery = await _contentRepository.GetAll(p => p.Id == activityDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                activityData.ContentId = activityDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.Activity,
                    ActivityList = new List<Activity>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                activityData.Content = content;
            }

            await _repository.InsertAsync(activityData);
            var contentId = activityData?.ContentId ?? content.Id;
            await _contentDocumentService.ProcessDocumentListAsync(activityDto.DocumentList, contentId, DocumentType.Activity);
            await _contentDocumentService.ProcessDocumentIdtListAsync(activityDto.DocumentIdtList, contentId);

            var message = activityDto.ContentId.HasValue ? "Etkinlik başarıyla eklendi." : "Etkinlik ve içerik başarıyla eklendi.";
            return new SuccessDataResult<ActivityDto>(activityDto, message);
        }
        public async Task<IDataResult<ActivityDto>> UpdateActivity(ActivityDto activityDto)
        {
            var unitId = activityDto.UnitId ?? UserInfoExtensions.GetUnitId();


            var existingactivityQuery = await _repository.GetAll(a => a.Id == activityDto.Id);
            var existingactivity = await existingactivityQuery.FirstOrDefaultAsync();
            if (existingactivity == null)
            {
                throw new NotFoundException("Güncellenmek istenen etkinlik bulunamadı.");
            }

            _mapper.Map(activityDto, existingactivity);
            existingactivity.SeoTitle = existingactivity.Title.ToStringForSeo();
            existingactivity = BaseEntityHelper.SetBaseEntitiy(existingactivity);
            Content content = null;

            if (activityDto.ContentId.HasValue)
            {
                var contentQuery = await _contentRepository.GetAll(p => p.Id == activityDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                existingactivity.ContentId = activityDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.Activity,
                    ActivityList = new List<Activity>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                existingactivity.Content = content;
            }


            await _repository.UpdateAsync(existingactivity);
            var contentId = existingactivity?.ContentId ?? content.Id;


            await _contentDocumentService.UpdateDocumentListAsync(activityDto.DocumentList, contentId, DocumentType.Activity);
            await _contentDocumentService.UpdateDocumentIdtListAsync(activityDto.DocumentIdtList, contentId);

            return new SuccessDataResult<ActivityDto>(activityDto, "Etlinlik başarıyla güncellendi.");
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

                var content = (await _contentRepository.GetAll(c => c.Id == contentId)).FirstOrDefault();
                if (content != null)
                {
                    await _contentRepository.DeleteAsync(content);
                }
            }
        }
        public async Task<ActivityDto> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var activity = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var activityDto = _mapper.Map<ActivityDto>(activity);
            activityDto.DocumentList = activity?.Content?.ContentDocuments
                                            .Where(d => d.Document != null)
                                           .Select(d => new DocumentSummeryDto
                                           {
                                               Path = d.Document.Path.ToFullUrl(),
                                               Description = d.Document.Description,
                                               Name = d.Document.Name,
                                               Id = d.Document.Id
                                           })
                                           .ToList() ?? new List<DocumentSummeryDto>();
            return activityDto;
        }
        public async Task<ListModel<ActivityDto>> GetByContentId(Guid ContentId, CancellationToken token)
        {
            var listModel = new ListModel<ActivityDto>();
            var news = await _repository.GetAll(p => p.ContentId == ContentId);

            listModel.Items = await news.ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).ToListAsync(token);


            return listModel;
        }

        #region Helper Methods
        private async Task<bool> IsActivityExistInSameLanguageAsync(ActivityDto activityDto)
        {
            var isExistQuery = await _repository.GetAll(a => a.LanguageId == activityDto.LanguageId);

            if (activityDto.ContentId.HasValue)
            {
                isExistQuery = isExistQuery.Where(a => a.ContentId == activityDto.ContentId);
            }
            else
            {

                isExistQuery = isExistQuery.Where(a => a.Title == activityDto.Title);
            }

            return await isExistQuery.AnyAsync();
        }
        #endregion



    }
}
