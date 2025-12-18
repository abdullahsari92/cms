using AS.Entities.PublicUI.Base;
using System;

namespace AS.Entities.PublicUI.Dtos.Menu
{
    public class MenuListDtoUI : DtoUI
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }
    }
}
