using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;


namespace AS.Business.Interfaces
{
    public interface IAnnouncementService : IBaseService<Announcement, AnnouncementDto>
    {
        Task<IDataResult<AnnouncementDto>> AddAnnouncements(AnnouncementDto announcementDto);
        Task<IDataResult<AnnouncementDto>> UpdateAnnouncement(AnnouncementDto announcementDto);
        Task Delete(Guid contentId, Guid languageId);
        Task<AnnouncementDto> GetById(Guid contentId);
        Task<ListModel<AnnouncementDto>> GetByContentId(Guid ContentId, CancellationToken token);

    }
}
