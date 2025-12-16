using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;


namespace AS.Business.Interfaces
{
    public interface IUnitsService: IBaseService<Units,UnitsDto>
    {
        Task<IDataResult<UnitsDto>> AddUnits( UnitsDto unitsDto);
        Task<IDataResult<UnitsDto>> UpdateUnits(UnitsDto unitsDto);
        Task<UnitModel> GetById(Guid id);
        Task<UnitModel> GetUnitByUrl(string unitUrl, CancellationToken token);

    }
}
