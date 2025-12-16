using AS.Core;
using AS.Entities.Entity;

namespace AS.Entities.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Veri tabanı nesneleri için temel nesne
    /// </summary>
    public class BaseEntity: IEntity
    {
        /// <summary>
        /// Birincil anahtar
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Oluşturulma zamanı
        /// </summary>
        public DateTime CreationTime { get; set; }


        //public int CreatedById { get; set; }
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public  Guid CreatedById { get; set; }


        public virtual User CreatedBy { get; set; }


        /// <summary>
        /// Son değişiklik zamanı
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Son değiştiren kullanıcı
        /// </summary>
        public  Guid? UpdatedById { get; set; }

        public virtual User UpdatedBy { get; set; }


        /// <summary>
        /// Onaylı kayıt mı?
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        ///Silindi m?
        /// </summary>
        public bool IsDeleted { get; set; }


    }

}
