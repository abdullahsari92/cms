using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Entities.Base;
using AS.Entities.Simple;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AS.Business
{
    public class BaseManager<TEntity, TMapTo> : IBaseService<TEntity, TMapTo>
        where TEntity : class, IEntity, new()
        where TMapTo : class, IDto, new()
    {

        protected readonly IRepository<TEntity> _repository;
        protected IMapper _mapper;
        protected readonly IRedisService _redisService;


        public BaseManager(IRepository<TEntity> repository, IMapper mapper, IRedisService redisService)
        {
            _repository = repository;
            _mapper = mapper;
            _redisService = redisService;
        }


        public async Task<ListModel<TMapTo>> BaseGetAll(CancellationToken token = default)
        {           
                var listModel = new ListModel<TMapTo>();
                var entitys = await _repository.GetAll();
                listModel.Items = await entitys.ProjectTo<TMapTo>(_mapper.ConfigurationProvider).ToListAsync(token);
                return listModel;         

        }

        public async Task<List<NameValue>> BaseGetSelectOptions()
        {
           var deger = typeof(TEntity);

            var key = _redisService.BuildCacheKey(deger.Name+ "-options");
            var listModel = await _redisService.GetAsync<List<NameValue>>(key);

            if(listModel == null || listModel.Count ==0)
            {
                var entitys = await _repository.GetAll();
                listModel = await entitys.Where(p=>p.IsApproved).ProjectTo<NameValue>(_mapper.ConfigurationProvider).ToListAsync();

                var success = await _redisService.SetAsync<List<NameValue>>(key, listModel);

            }


            return listModel;
        }

  

        public async Task<TMapTo?> BaseGetById(Guid id)
        {           
                var entity = await _repository.GetAll(p => p.Id == id);

                if (entity == null)
                {
                    throw new Exception("Kayýt bulunamadý.");
                }
               return await  entity.ProjectTo<TMapTo>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
          
        }

        public async Task<TMapTo> BaseInsert(TMapTo model, bool autoSaveIsNotActive = false)
        {
           var tEntity = _mapper.Map(model, new TEntity());

            tEntity = BaseEntityHelper.SetBaseEntitiy(tEntity);

            tEntity.Id =tEntity.Id ==Guid.Empty ? Guid.NewGuid(): tEntity.Id;
            tEntity.IsApproved = true;
            tEntity.UpdateTime = null;
            return _mapper.Map(await _repository.InsertAsync(tEntity, autoSaveIsNotActive), new TMapTo());
        }

        public async Task<int> BaseInsertRange(List<TMapTo> model)
        {
            var listEntity = new List<TEntity>();
            foreach (var item in model)
            {
                var tEntity = _mapper.Map(item, new TEntity());
                tEntity = BaseEntityHelper.SetBaseEntitiy(tEntity);
                tEntity.Id = Guid.NewGuid();
                tEntity.IsApproved = true;
                listEntity.Add(tEntity);
            }
          return  await  _repository.InsertRangeAsync(listEntity);

                  
        }
        public async Task<TMapTo> BaseUpdate(TMapTo model)
        {

            var tEntity = _repository.Get(p => p.Id == model.Id);

            if(tEntity == null)
            {

                return null;
            }
            tEntity = _mapper.Map(model, tEntity);

            tEntity = BaseEntityHelper.SetBaseUpdateEntitiy(tEntity);


            return _mapper.Map(await _repository.UpdateAsync(tEntity), new TMapTo());
        }
        public async Task<bool> BaseIsExist(Expression<Func<TEntity, bool>>? filter)
        {
            return _repository.IsExist(filter);   
        }

        public async Task BaseDelete(Guid id)
        {
           await _repository.DeleteAsync(id);

        }
    }

}
