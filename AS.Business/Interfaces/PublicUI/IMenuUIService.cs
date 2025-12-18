using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Menu;

namespace AS.Business.Interfaces.PublicUI
{
    public interface IMenuUIService
        : IBaseUIService<Menu, MenuDtoUI>
    {
        Task<List<MenuListDtoUI>> List( Guid? languageId);
        // Doğru imza:
        Task<MenuDtoUI?> GetById(Guid id);
    }
}
