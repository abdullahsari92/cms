using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Menu;
using AutoMapper;
using Business.Adapters.Redis;
using Microsoft.EntityFrameworkCore;

namespace AS.Business.PublicUIManager
{
    public class MenuUIManager
        : BaseUIManager<Menu, MenuDtoUI>, IMenuUIService
    {
        public MenuUIManager(
            IRepository<Menu> repository,
            IMapper mapper,
            IRedisService redisService
        ) : base(repository, mapper, redisService)
        {
        }

        // 🔹 Public Menü Listesi
        public async Task<List<MenuListDtoUI>> List( Guid? languageId)
        {
            // Default Language
            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;


            // Menüleri filtrele
            var query = await _repository.GetAll(m =>
                m.LanguageId == langId &&

                m.IsApproved == true
            );

            // Slider'daki Select mantığının MENU karşılığı
            var menuList = await query
                .OrderBy(m => m.DisplayOrder)
                .Select(m => new MenuListDtoUI
                {
                    Id = m.Id,

                    Name = m.Name,
                    Url = m.Url,
                    Icon = m.Icon,
                    DisplayOrder = m.DisplayOrder
                })
                .ToListAsync();

            return menuList;
        }


        // 🔹 Menü Detay
        public async Task<MenuDtoUI?> GetById(Guid id)
        {
            var queryable = await _repository.GetAll(m => m.Id == id);

            var menu = await queryable.FirstOrDefaultAsync();

            if (menu == null)
                return null;

            var menuDto = _mapper.Map<MenuDtoUI>(menu);
            return menuDto;
        }
    }
}
