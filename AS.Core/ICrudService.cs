
using AS.Core.ValueObjects;

namespace AS.Core;

/// <summary>
/// CRUD iþlemleri yapan sýnýflar için arayüz
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrudService<T>  where T : class, new()
{

    /// <summary>
    /// Filtreleme yaparak birden çok satýr içeren liste modelini döndürür.
    /// </summary>
    /// <param name="filterModel">Filtreleme Ýçin Sýnýf</param>
    /// <returns>T türünden liste modeli</returns>
    Task<ListModel<T>> GetAll();

    /// <summary>
    /// ID parametresi alarak tek satýr içeren detay modelini döndürür.
    /// </summary>
    /// <param name="id">ID parametresi</param>
    /// <returns></returns>
    Task<T> GetById(Guid id);

    /// <summary>
    /// Ekleme iþlemini yaparak sonucu modelle döndürür.
    /// </summary>
    /// <param name="addModel"></param>
    /// <returns>Ekleme iþlemi sonucu oluþan model</returns>
    Task<T> Insert(T model);

    /// <summary>
    /// Güncelleme iþlemini yaparak sonucu modelle döndürür.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Güncelleme iþlemi sonucu oluþan model</returns>
    void Update(T model);

    /// <summary>
    /// ID parametresi alarak silme iþlemini yapar.
    /// </summary>
    /// <param name="id">ID parametresi</param>
    void Delete(Guid id);

}
