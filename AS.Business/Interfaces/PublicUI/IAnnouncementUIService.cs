using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Announcement;


namespace AS.Business.Interfaces.PublicUI
{
    public interface IAnnouncementUIService : IBaseUIService<Announcement,AnnouncementDtoUI >
    {
        Task<List<AnnouncementListDtoUI>> List(Guid? UnitId,Guid? languageId);
        Task<AnnouncementDtoUI> GetById(Guid id);
    }
}
