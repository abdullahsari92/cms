
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;


namespace AS.Entities.PublicUI.Dtos.Activity
{
    public class ActivityListDtoUI : DtoUI
    {
        public string Title { get; set; }
        public ActivityCategory ActivityCategory { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public DateTime ActivityEndDate { get; set; }
    }
}
