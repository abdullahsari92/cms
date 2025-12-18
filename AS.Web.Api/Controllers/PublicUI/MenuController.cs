using AS.Business;
using AS.Business.Interfaces.PublicUI;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.PublicUI.Dtos.Menu;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers.PublicUI
{
    [ApiController]
    [Route("api/public/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "is-Public")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuUIService _menuUIService;

        public MenuController(IMenuUIService menuUIService)
        {
            _menuUIService = menuUIService;
        }

        /// <summary>
        /// Public Menü Listesi
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<SuccessDataResult<List<MenuListDtoUI>>>> List( Guid? languageId)
        {
            var menus = await _menuUIService.List( languageId);
            return new SuccessDataResult<List<MenuListDtoUI>>(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuccessDataResult<MenuDtoUI>>> GetById(Guid id)
        {
            var menu = await _menuUIService.GetById(id);
            return new SuccessDataResult<MenuDtoUI>(menu);
        }

    }
}
