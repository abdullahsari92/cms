using AS.Entities.Base;
using Microsoft.EntityFrameworkCore;
using ServiceStack;

namespace AS.Entities.Entity
{
    /// <inheritdoc />
    /// <summary>
    /// AS framework tüm tabloların history tutmak için yapılmıştır.
    /// </summary>
    public class History 
    {

        public Guid Id { get; set; }
        public Guid TransactionerUserId { get; set; }
        public User TransactionerUser { get; set; }

        public DateTime TransactionerTime { get; set; }
        public string Data { get; set; }

        public Guid? EntityId { get; set; }

        public string EntityName { get; set; }
        public string EntityState { get; set; }


        





    }
}
