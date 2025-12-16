using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;

namespace AS.Business.Interfaces
{
    public interface IFacultyService: IBaseService<Faculty, FacultyDto>
    {
        Task<List<NameValue>> GetSelectOptions();

        Task<FacultyDto> BaseGetByCode(string code);

        Task<bool> SetupFacultyGetOBS();

    }
}
