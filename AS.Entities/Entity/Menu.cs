using AS.Entities.Base;

namespace AS.Entities.Entity
{
    /// <inheritdoc />
    /// <summary>
    /// AS framework ile geliştirilen sunucuda barındırılan ve yetki sistemine dahil edilen uygulamaların menuleri için sınıf
    /// </summary>
    public class Menu : BaseEntity
    {
        /// <summary>
        /// Menunun görünen adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Menunun adresi
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Menunun ikon bilgisi
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Menunun açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Sıra No
        /// </summary>
        public int DisplayOrder { get; set; }

        public Guid? ParentId { get; set; }


        /// <summary>
        /// Menunun üst menusu
        /// </summary>
        public virtual Menu Parent { get; set; }

        public Guid? PageId { get; set; }
        public Pages Pages { get; set; }

        public Guid?  UnitId { get; set; }
        public Units Units { get; set; }

        public Guid? LanguageId { get; set; }
        public Language Language { get; set; }

        /// <summary>
        /// Menunun alt menuleri
        /// </summary>
        public virtual ICollection<Menu> Children { get; set; }

        /// <summary>
        /// Menunun rollerle ilişkileri
        /// </summary>
        public virtual ICollection<PermissionMenuLine> PermissionMenuLines { get; set; }
    }
}