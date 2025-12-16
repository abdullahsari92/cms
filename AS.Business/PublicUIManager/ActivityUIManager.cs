using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Core.Helpers;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Activity;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business.PublicUIManager
{
    public class ActivityUIManager : BaseUIManager<Activity, ActivityDtoUI>, IActivityUIService
    {

        public ActivityUIManager(IRepository<Activity> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {

        }
        public async Task<List<ActivityListDtoUI>> List(Guid? UnitId, Guid? languageId)
        {
            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;

            var defaultUnitId = new Guid("e0f90674-f6b6-4370-8f0e-1fb93124d115");
            var unitId = UnitId ?? defaultUnitId;

            var query = await _repository.GetAll(x => x.LanguageId == langId
                                                      && x.Content.UnitId != null
                                                      && x.Content.UnitId == unitId
                                                     );

            var activityDtoList = _mapper.Map<List<ActivityListDtoUI>>(query.ToList());
            return activityDtoList;
        }


        public async Task<ActivityDtoUI> GetById(Guid id)
        {
            var query = await _repository.GetAll(a => a.Id == id);
            var activity = await query
                                    .Include(a => a.Content)
                                    .ThenInclude(c => c.ContentDocuments)
                                    .ThenInclude(d => d.Document)
                                    .FirstOrDefaultAsync();

            var activityDto = _mapper.Map<ActivityDtoUI>(activity);
            activityDto.DocumentList = activity?.Content?.ContentDocuments
                                                .Where(d => d.Document != null)
                                               .Select(d => new DocumentSummeryDto
                                               {
                                                   Path = d.Document.Path.ToFullUrl(),
                                                   Description = d.Document.Description,
                                                   Name = d.Document.Name,
                                                   Id=d.Document.Id
                                               })
                                               .ToList() ?? new List<DocumentSummeryDto>();

            return activityDto;
        }

    }
}
