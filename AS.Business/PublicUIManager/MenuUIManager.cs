using AS.Business.Interfaces.PublicUI;
using AS.Core;
using AS.Entities.Entity;
using AS.Entities.PublicUI.Dtos.Menu;
using AS.Entities.PublicUI.Dtos.Pages;
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

        // ======================================================
        // 🔹 PUBLIC MENU LIST
        // ======================================================
        public async Task<List<MenuListDtoUI>> List(Guid? languageId)
        {
            var defaultLanguageId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var langId = languageId ?? defaultLanguageId;

            var query = await _repository.GetAll(m =>
                m.LanguageId == langId &&
                m.IsApproved == true
            );

            var menuList = await query
                .Include(m => m.Pages)
                .Include(m => m.Children)
                .OrderBy(m => m.DisplayOrder)
                .Select(m => new MenuListDtoUI
                {
                    Id = m.Id,
                    Name = m.Name,
                    Icon = m.Icon,
                    DisplayOrder = m.DisplayOrder,

                    Page = m.Pages == null ? null : new PagesDtoUI
                    {
                        Id = m.Pages.Id,
                        Title = m.Pages.Title,
                        SeoTitle = m.Pages.SeoTitle,
                        ExternalUrl = m.Pages.ExternalUrl
                    },

                    Children = m.Children
                        .OrderBy(c => c.DisplayOrder)
                        .Select(c => new MenuListDtoUI
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Icon = c.Icon,
                            DisplayOrder = c.DisplayOrder
                        })
                        .ToList()
                })
                .ToListAsync();

            return menuList;
        }

        // ======================================================
        // 🔹 MENU DETAIL
        // ======================================================
        public async Task<MenuDtoUI?> GetById(Guid id)
        {
            var queryable = await _repository.GetAll(m => m.Id == id);

            var menu = await queryable.FirstOrDefaultAsync();

            if (menu == null)
                return null;

            return _mapper.Map<MenuDtoUI>(menu);
        }
    }
}
