using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Entities.Entity;
using AutoMapper;
using Business.Adapters.Redis;

namespace AS.Business
{
    public class SettingManager : BaseManager<Setting, SettingDto>, ISettingService
    {
           

        public SettingManager(IMapper mapper, IRepository<Setting> repository, IRedisService redisService) : base(repository, mapper, redisService)
        {
        }

        public async Task<SettingDto> GetByCode(string code)
        {
            var query = await _repository.GetAll(p => p.Code == code);

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                throw new NotFoundException();
            }

       return  _mapper.Map(entity, new SettingDto());

      
        }

    }

}
