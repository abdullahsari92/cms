using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class LanguageManager : BaseManager<Language, LanguageDto>, ILanguageService
    {
        public LanguageManager(IRepository<Language> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }

        public async Task<List<NameValue>> GetSelectOptionsLanguage()
        {
            var query = await _repository.GetAll();
            var listModel = await query
                .Select(lang => new NameValue
                {
                    Name = lang.Name,    
                    Value = lang.Id.ToString(),
                    Selected = lang.Code == "Tr",

                })
                .ToListAsync();

            return listModel;
        }


    }

}
