using System.Linq.Expressions;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Base;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Base;
using AS.Entities.Simple;

namespace AS.Business.Interfaces.PublicUI
{
    public interface IBaseUIService<TEntity, TMapTo>
      where TEntity : class, IEntity, new()
      where TMapTo : class, IDtoUI, new()
    {
            
        /// <summary>
        /// Filtreleme yaparak birden çok satýr içeren liste modelini döndürür.
        /// </summary>
        /// <param name="filterModel">Filtreleme Ýçin Sýnýf</param>
        /// <returns>T türünden liste modeli</returns>
        Task<ListModel<TMapTo>> BaseGetAll(CancellationToken token);

        /// <summary>
        /// Selectbox doldurmak için kullanýlabilir.
        /// </summary>
        /// <param name="filterModel">Filtreleme Ýçin Sýnýf</param>
        /// <returns>T türünden liste modeli</returns>
        Task<List<NameValue>> BaseGetSelectOptions();

        /// <summary>
        /// ID parametresi alarak tek satýr içeren detay modelini döndürür.
        /// </summary>
        /// <param name="id">ID parametresi</para m>
        /// <returns></returns>
        Task<TMapTo> BaseGetById(Guid id);

        /// <summary>
        /// Filtre þartýna göre kayýt varmý kontrolü
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Bool deðer döndürüyor</returns>
        Task<bool> BaseIsExist(Expression<Func<TEntity, bool>>? filter);
    }
}

