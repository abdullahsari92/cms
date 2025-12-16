using AS.Entities.Base;
using AS.Entities.Enums;


namespace AS.Entities.Entity
{
    public class Content : BaseEntity
    {
        public Guid UnitId { get; set; }
        public Units Units { get; set; }
        public ContentCategory ContentCategory { get; set; }

        public ICollection<News> NewsList { get; set; }
        public ICollection<Pages> PagesList { get; set; }
        public ICollection<Activity> ActivityList { get; set; }
        public ICollection<Announcement> AnnouncementList { get; set; }
        public ICollection<ContentDocument> ContentDocuments { get; set; }

    }
}
