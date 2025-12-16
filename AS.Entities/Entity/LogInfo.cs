
namespace AS.Entities.Entity
{
    /// <inheritdoc />
    /// <summary>
    /// AS framework tüm hataların loglarıno tutmak için yapılmıştır.
    /// </summary>
    public class LogInfo
    {

        public Guid Id { get; set; }
        public Guid TransactionerUserId { get; set; }

        public DateTime TransactionerTime { get; set; }
        public string Template { get; set; }

        public string Message { get; set; }
        public string ControllerActionName { get; set; }




    }
}
