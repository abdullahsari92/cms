using AS.Entities.Dtos;
using AS.Entities.Entity;


namespace AS.Business.Interfaces
{
    public interface ILogInfoService
    {
        Task<List<LogInfoDto>> GetAllLogInfoAsync();
    }
}
