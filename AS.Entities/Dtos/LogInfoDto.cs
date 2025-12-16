

namespace AS.Entities.Dtos
{
    public class LogInfoDto
    {
        public Guid TransactionerUserId { get; set; }
        public string Template { get; set; }
        public string Email { get; set; }
        public DateTime TransactionerTime { get; set; }
        public string Message { get; set; }
        public string ControllerActionName { get; set; }
    }
}
