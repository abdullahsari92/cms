using AS.Entities.PublicUI.Base;
using AS.Entities.PublicUI.Dtos.Pages;
using System.Collections.Generic;

namespace AS.Entities.PublicUI.Dtos.Menu
{
    public class MenuListDtoUI : DtoUI
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int DisplayOrder { get; set; }

        // 🔥 PAGE (SEO için)
        public PagesDtoUI? Page { get; set; }

        // 🔥 ALT MENÜLER
        public List<MenuListDtoUI> Children { get; set; } = new();
    }
}
