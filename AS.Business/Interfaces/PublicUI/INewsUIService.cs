using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Activity;
using AS.Entities.PublicUI.Dtos.Announcement;
using AS.Entities.PublicUI.Dtos.News;


namespace AS.Business.Interfaces.PublicUI
{
    public interface INewsUIService:IBaseUIService<News,NewsDtoUI>
    {
        Task<List<NewsListDtoUI>> List(Guid? UnitId, Guid? languageId);
        Task<NewsDtoUI> GetById(Guid id);
    }
}
