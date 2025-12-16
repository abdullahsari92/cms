using AS.Business.Interfaces;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;

namespace AS.Business
{
    public interface IPersonService: IBaseService<Person, PersonDto>
    {

       // Task<PersonDto> Insert(PersonDto ersonDtoDto);
        Task<IDataResult<PersonDto>> Insert(PersonDto personDto);

        Task<PersonDto> GetById(Guid id);

        Task<ListModel<PersonDto>> GetAll(CancellationToken token);

         Task Update(PersonDto personDto);
        //Task<List<NameValue>> GetResponsibleTeacherSelectOptionsByFacultyId(Guid facultyId);

        Task Delete(Guid userId);

    }
}