using AS.Business.Interfaces;
using AS.Data;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class LogInfoManager : ILogInfoService
    {
        private readonly EfDbContext _context;
        protected IMapper _mapper;

        public LogInfoManager(EfDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LogInfoDto>> GetAllLogInfoAsync()
        {
            var fiveDaysAgo = DateTime.UtcNow.AddDays(-5);

            var logInfo = await (from log in _context.Set<LogInfo>()
                                 join user in _context.Set<User>()
                                 on log.TransactionerUserId equals user.Id
                                 where log.TransactionerTime >= fiveDaysAgo
                                 && log.TransactionerUserId != Guid.Empty
                                 orderby log.TransactionerTime
                                 select new LogInfoDto
                                 {
                                     TransactionerUserId = log.TransactionerUserId,
                                     Email = user.Email,
                                     Template = log.Template,
                                     TransactionerTime = log.TransactionerTime,
                                     Message = log.Message,
                                     ControllerActionName = log.ControllerActionName
                                 })
                                 .ToListAsync();

            return logInfo ?? new List<LogInfoDto>();
        }

    }
}
