
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;

namespace AS.Entities.PublicUI.Dtos.Announcement
{
    public class AnnouncementListDtoUI:DtoUI
    {
        public string Title { get; set; }
        public DateTime PublishBeginDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
    }
}
