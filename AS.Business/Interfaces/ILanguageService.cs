using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;

namespace AS.Business.Interfaces
{
    public interface ILanguageService : IBaseService<Language, LanguageDto>
    {
        Task<List<NameValue>> GetSelectOptionsLanguage();
    }
}
