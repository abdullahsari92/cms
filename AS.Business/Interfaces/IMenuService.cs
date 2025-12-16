using AS.Business.Interfaces;
using AS.Core;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;

namespace AS.Business
{
    public interface IMenuService: IBaseService<Menu, MenuDto>
    {
        Task<List<MenuDto>> GetAllCategories(CancellationToken token); //Tüm Menüler 
        Task<List<MenuDto>> GetMainCategories(CancellationToken token); //Sadece Ana Kategoriler
        Task<List<MenuDto>> GetCategoriesByCategoryId(Guid id);//Id ye göre kategori getirme
        Task<IDataResult<MenuDto>> AddCategory(MenuDto menuDto); //Kategori Ekleme
        Task<IDataResult<MenuDto>> UpdateParentId(Guid id, Guid? parentId); //Parent guncelleme
        Task<IDataResult<MenuDto>> UpdateCategory(Guid id,MenuDto menuDto); //Menu guncelleme
        Task DeleteMenu(Guid id); //menu silme
        Task<IDataResult<MenuDto>> AddSubCategory(Guid id,MenuDto menuDto); //Alt Kategori Ekleme
    }

}