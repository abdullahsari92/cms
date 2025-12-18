using AS.Entities.PublicUI.Base;
using System;
using System.Collections.Generic;

namespace AS.Entities.PublicUI.Dtos.Menu
{
    public class MenuDtoUI : DtoUI
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }

        public Guid? ParentId { get; set; }

        public List<MenuDtoUI> Children { get; set; }
    }
}
