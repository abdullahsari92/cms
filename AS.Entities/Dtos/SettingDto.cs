using AS.Entities.Base;

namespace AS.Entities.Entity
{
    public class SettingDto : Dto
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

    }

}
