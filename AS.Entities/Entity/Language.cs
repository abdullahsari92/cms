using AS.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Entities.Entity
{
    public class Language:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string SeoCode { get; set; }

        public ICollection<News> NewsList { get; set; }
        public ICollection<Announcement> AnnouncementList { get; set; }
        public ICollection<Activity> ActivityList { get; set; }
        public ICollection<Pages> PagesList { get; set; }
        public ICollection<Slider> SliderList { get; set; }
        public ICollection<Menu> MenuList { get; set; }
    }
}
