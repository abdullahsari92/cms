using AS.Entities.Dtos;
using AS.Entities.Models;
using AS.Entities.Simple;

namespace AS.Business.Interfaces
{
    public interface IAccountService 
    {

        Task<PersonDto> GetProfile(CancellationToken token);

        Task<PersonDto> UpdateProfile(PersonDto personDto);
        Task PasswordChange(PasswordChangeModel model);
        Task<PersonDto> GetById(Guid id);
        Task<List<NameValue>> GetSelectOptionsDepartmans(string email);


    }
}
