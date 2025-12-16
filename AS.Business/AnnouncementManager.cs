using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;


namespace AS.Business
{
    public class AnnouncementManager : BaseManager<Announcement, AnnouncementDto>, IAnnouncementService
    {
        private readonly IRepository<Content> _repositoryContent;
        private IContentDocumentService _contentDocumentService;
        public AnnouncementManager(IRepository<Announcement> repository, IMapper mapper, IRedisService redisService, IRepository<Content> repositoryContent = null, IContentDocumentService contentDocumentService = null) : base(repository, mapper, redisService)
        {
            _repositoryContent = repositoryContent;
            _contentDocumentService = contentDocumentService;
        }

        public async Task<IDataResult<AnnouncementDto>> AddAnnouncements(AnnouncementDto announcementDto)
        {

            var unitId = announcementDto.UnitId ?? UserInfoExtensions.GetUnitId();





            var isExist = await IsAnnouncementExistInSameLanguageAsync(announcementDto);
            if (isExist)
            {
                throw new DuplicateException("Bu dilde bu içerik zaten mevcut.");
            }

            Content content = null;
            Announcement announcementData = _mapper.Map<Announcement>(announcementDto);
            announcementData.Id = Guid.NewGuid();
            announcementData.SeoTitle = announcementData.Title.ToStringForSeo();
            announcementData = BaseEntityHelper.SetBaseEntitiy(announcementData);

            //ContentId geldiği durumlarda.
            if (announcementDto.ContentId.HasValue)
            {
                var contentQuery = await _repositoryContent.GetAll(p => p.Id == announcementDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                announcementData.ContentId = announcementDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.Announcement,
                    AnnouncementList = new List<Announcement>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                announcementData.Content = content;
            }

            await _repository.InsertAsync(announcementData);
            var contentId = announcementData?.ContentId ?? content.Id;
            await _contentDocumentService.ProcessDocumentListAsync(announcementDto.DocumentList, contentId, DocumentType.Announcement);
            await _contentDocumentService.ProcessDocumentIdtListAsync(announcementDto.DocumentIdtList, contentId);

            var message = announcementDto.ContentId.HasValue ? "Duyuru başarıyla eklendi." : "Duyuru ve içerik başarıyla eklendi.";
            return new SuccessDataResult<AnnouncementDto>(announcementDto, message);
        }
        public async Task<IDataResult<AnnouncementDto>> UpdateAnnouncement(AnnouncementDto announcementDto)
        {
            var unitId = announcementDto.UnitId ?? UserInfoExtensions.GetUnitId();


            var existingannouncementyQuery = await _repository.GetAll(a => a.Id == announcementDto.Id);
            var existingannouncement = await existingannouncementyQuery.FirstOrDefaultAsync();
            if (existingannouncement == null)
            {
                throw new NotFoundException("Güncellenmek istenen Duyuru bulunamadı.");
            }

            _mapper.Map(announcementDto, existingannouncement);
            existingannouncement.SeoTitle = existingannouncement.Title.ToStringForSeo();
            existingannouncement = BaseEntityHelper.SetBaseEntitiy(existingannouncement);
            Content content = null;

            if (announcementDto.ContentId.HasValue)
            {
                var contentQuery = await _repositoryContent.GetAll(p => p.Id == announcementDto.ContentId.Value);
                content = await contentQuery.FirstOrDefaultAsync();
                existingannouncement.ContentId = announcementDto.ContentId.Value;
            }
            else
            {
                content = new Content
                {
                    Id = Guid.NewGuid(),
                    UnitId = unitId.Value,
                    ContentCategory = ContentCategory.Announcement,
                    AnnouncementList = new List<Announcement>()
                };
                content = BaseEntityHelper.SetBaseEntitiy(content);
                existingannouncement.Content = content;
            }


            await _repository.UpdateAsync(existingannouncement);
            var contentId = existingannouncement?.ContentId ?? content.Id;


            await _contentDocumentService.UpdateDocumentListAsync(announcementDto.DocumentList, contentId, DocumentType.Announcement);
            await _contentDocumentService.UpdateDocumentIdtListAsync(announcementDto.DocumentIdtList, contentId);

            return new SuccessDataResult<AnnouncementDto>(announcementDto, "Duyuru başarıyla güncellendi.");
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
        public async Task<AnnouncementDto> GetById(Guid id)
        {

            var query = await _repository.GetAll(a => a.Id == id);
            var announcement = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var announcementDto = _mapper.Map<AnnouncementDto>(announcement);
            announcementDto.DocumentList = announcement?.Content?.ContentDocuments
                                            .Where(d => d.Document != null)
                                           .Select(d => new DocumentSummeryDto
                                           {
                                               Path = d.Document.Path.ToFullUrl(),
                                               Description = d.Document.Description,
                                               Name = d.Document.Name,
                                               Id = d.Document.Id
                                           })
                                           .ToList() ?? new List<DocumentSummeryDto>();
            return announcementDto;
        }
        public async Task<ListModel<AnnouncementDto>> GetByContentId(Guid ContentId, CancellationToken token)
        {

            var listModel = new ListModel<AnnouncementDto>();
            var news = await _repository.GetAll(p => p.ContentId == ContentId);

            listModel.Items = await news.ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider).ToListAsync(token);


            return listModel;
        }

        #region Helper Methods
        private async Task<bool> IsAnnouncementExistInSameLanguageAsync(AnnouncementDto announcementDto)
        {
            var isExistQuery = await _repository.GetAll(a => a.LanguageId == announcementDto.LanguageId);

            if (announcementDto.ContentId.HasValue)
            {
                isExistQuery = isExistQuery.Where(a => a.ContentId == announcementDto.ContentId);
            }
            else
            {
                isExistQuery = isExistQuery.Where(a => a.Title == announcementDto.Title);
            }

            return await isExistQuery.AnyAsync();
        }


        #endregion

    }
}

