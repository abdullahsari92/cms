using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Activity;


namespace AS.Business.Interfaces.PublicUI
{
    public interface IActivityUIService : IBaseUIService<Activity, ActivityDtoUI>
    {
        Task<List<ActivityListDtoUI>> List(Guid? UnitId,Guid? languageId);
        Task<ActivityDtoUI> GetById(Guid id);
    }
}
