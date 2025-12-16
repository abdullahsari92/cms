using AS.Business;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]

    public class MenuController : ControllerBase
    {
        private IMenuService _menuService;

        //menu çalışması devam ediyor
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // Tüm Menü
        [HttpGet]
        public async Task<ActionResult<Core.IResult>> GetAllCategories(CancellationToken token)
        {
       
            var menuDtos = await _menuService.GetAllCategories(token);
            return new SuccessDataResult<List<MenuDto>>(menuDtos);
        }


        // Ana Menü
        [HttpGet]
        public async Task<ActionResult<Core.IResult>> GetMainCategories(CancellationToken token)
        {
            var menuDtos = await _menuService.GetMainCategories(token);
            return new SuccessDataResult<List<MenuDto>>(menuDtos);
        }

        // Menü ID ile Menüler
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetCategoriesByCategoryId(Guid id)
        {
            var menuDtos = await _menuService.GetCategoriesByCategoryId(id);
            return new SuccessDataResult<List<MenuDto>>(menuDtos);
        }

        // Menü Ekleme
        [HttpPost]
        public async Task<ActionResult<Core.IResult>> AddCategory(MenuDto menuDto)
        {
            await _menuService.AddCategory(menuDto);
            return new SuccessDataResult<MenuDto>();
        }
        // SubMenü(Alt Menü) Ekleme
        [HttpPost("{id}")]
        public async Task<ActionResult<Core.IResult>> SubMenuAdd(Guid id, MenuDto menuDto)
        {
            await _menuService.AddSubCategory(id, menuDto);
            return new SuccessDataResult<MenuDto>();
        }
        //ParentId Güncelleme
        [HttpPost("{id}")]
        public async Task<ActionResult<Core.IResult>> update(Guid id, Guid? parentId)
        {
         await _menuService.UpdateParentId(id, parentId);
            return new SuccessDataResult<MenuDto>();
        }
        //Menü Güncelleme
        [HttpPost("{id}")]
        public async Task<ActionResult<Core.IResult>> UpdateCategory(Guid id, [FromBody] MenuDto menuDto)
        {
            await _menuService.UpdateCategory(id, menuDto);
            return Ok(new SuccessResult("İşlem Başarılı"));
        }
        //Menü Silme -Soft silme yapıyor.
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenu(Guid id)
        {
            await _menuService.DeleteMenu(id);

            return Ok(new SuccessDataResult<bool>(true));
        }



    }
}

