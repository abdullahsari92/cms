namespace AS.Core
{
    public interface IEntity
    {
        Guid Id { get; set; }


        public DateTime CreationTime { get; set; }


        //public int CreatedById { get; set; }
        /// <summary>
        /// Oluşturan kullanıcı
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Son değişiklik zamanı
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Son değiştiren kullanıcı
        /// </summary>
        public Guid? UpdatedById { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }



    }
}
