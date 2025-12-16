using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business.Interfaces
{
    public interface ILanguageDefinitionService: IBaseService<LanguageDefinition, LanguageDefinitionDto>
    {

        Task<bool> InsertAll(List<LanguageDefinitionDto> model);

        Task<ListModel<LanguageDefinitionDto>> GetLLanguageDefination(CancellationToken token);

    }
}
