using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Announcement;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;


namespace AS.Business.PublicUIManager
{
    public class AnnouncementUIManager : BaseUIManager<Announcement, AnnouncementDtoUI>, IAnnouncementUIService
    {
        public AnnouncementUIManager(IRepository<Announcement> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }
        public async Task<List<AnnouncementListDtoUI>> List(Guid? UnitId, Guid? languageId)
        {
            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;


            var defaultUnitId = new Guid("e0f90674-f6b6-4370-8f0e-1fb93124d115");
            var unitId = UnitId ?? defaultUnitId;

            var query = await _repository.GetAll(x => x.LanguageId == langId
                                                      && x.Content.UnitId != null
                                                      && x.Content.UnitId == unitId
                                                     );

            var annoList = _mapper.Map<List<AnnouncementListDtoUI>>(query.ToList());
            return annoList;
        }
        public async Task<AnnouncementDtoUI> GetById(Guid id)
        {

            var query = await _repository.GetAll(a => a.Id == id);
            var announcement = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var announcementDto = _mapper.Map<AnnouncementDtoUI>(announcement);
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
    }
}
