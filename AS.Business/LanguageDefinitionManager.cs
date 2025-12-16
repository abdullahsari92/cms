using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Entity;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business
{
    public class LanguageDefinitionManager : BaseManager<LanguageDefinition, LanguageDefinitionDto>,ILanguageDefinitionService
    {

        protected readonly IRedisService _redisService;


        public LanguageDefinitionManager(IRepository<LanguageDefinition> repositoryRole, IMapper mapper, IRedisService redisService) : base(repositoryRole, mapper, redisService)
        {
            _redisService = redisService;
        }



        public async Task<bool> InsertAll(List<LanguageDefinitionDto> model)
        {

            var  list = new List<LanguageDefinition>();
            foreach (LanguageDefinitionDto lang in model) {

                var language = _mapper.Map(lang,new LanguageDefinition());

                language = BaseEntityHelper.SetBaseEntitiy(language);

                language.Id = Guid.NewGuid();
                language.IsApproved = true;
                   

                if(!_repository.IsExist(p => p.Keyword == language.Keyword))
                {
                    list.Add(language);

                }

            }

             _repository.InsertRange(list);

            return true;
        }


        public async Task<ListModel<LanguageDefinitionDto>> GetLLanguageDefination(CancellationToken token)
        {
            var listModel = new ListModel<LanguageDefinitionDto>();

            var key = _redisService.BuildCacheKey("languageDefinitions");
            var languages = await _redisService.GetAsync<List<LanguageDefinitionDto>>(key);
               


            if (languages == null || languages.Count == 0)
            {
                var query = await _repository.GetAll();

                languages = await query.Select(p => new LanguageDefinitionDto
                {
                    Keyword = p.Keyword ?? "",
                    Tr = p.Tr ?? "",
                    En = p.En,
                    De = p.De,
                    Es = p.Es,
                    Fr = p.Fr,
                    Id = p.Id,
                }).ToListAsync(token);


                var success = await _redisService.SetAsync<List<LanguageDefinitionDto>>(key, languages);
                if (!success)
                {
                    // log
                }

            }

            listModel.Items = languages;
            return listModel;
        }


    }

}
