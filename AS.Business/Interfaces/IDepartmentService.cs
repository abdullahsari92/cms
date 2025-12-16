using System.Linq.Expressions;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;

namespace AS.Business.Interfaces
{
    public interface IDepartmentService: IBaseService<Department, DepartmentDto>
    {
        public Task<bool> SetupDeparmentGetOBS(CancellationToken token);

        Task<List<NameValue>> GetSelectOptions();


    }
}
