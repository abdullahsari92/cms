using AS.Entities.Base;
using AS.Entities.Entity;

namespace AS.Entities.Dtos
{
    public class MenuDto : Dto
    {


        /// <summary>
        /// Menunun görünen adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Menunun adresi
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Menunun ikon bilgisi
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Menunun açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Sıra No
        /// </summary>
        public int DisplayOrder { get; set; }

        public Guid? ParentId { get; set; }
        public Guid? UnitId { get; set; }

        /// <summary>
        /// Menunun üst menusu
        /// </summary>
        public MenuDto? Parent { get; set; }


        public Guid? PageId { get; set; }
        public Guid? LanguageId { get; set; }

        /// <summary>
        /// Menunun alt menuleri
        /// </summary>
        public List<MenuDto> Children { get; set; } = new List<MenuDto>();






    }
}
