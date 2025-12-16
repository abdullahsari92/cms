using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;



namespace AS.Business.Interfaces
{
    public interface IActivityService: IBaseService<Activity,ActivityDto>
    {
        Task<IDataResult<ActivityDto>> AddActivity(ActivityDto activityDto);
        Task<IDataResult<ActivityDto>> UpdateActivity(ActivityDto activityDto);
        Task Delete(Guid contentId, Guid languageId);
        Task<ActivityDto> GetById(Guid id);
        Task<ListModel<ActivityDto>> GetByContentId(Guid ContentId, CancellationToken token);
    }
}
