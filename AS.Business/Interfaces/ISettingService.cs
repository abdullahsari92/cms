using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface ISettingService: IBaseService<Setting, SettingDto>
    {

        Task<SettingDto> GetByCode(string code);

    }
}
