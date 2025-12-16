using AS.Business;
using AS.Core.ValueObjects;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AutoMapper;
using Business.Adapters.Redis;
using AS.Core.Helpers;

public class MenuManager : BaseManager<Menu, MenuDto> ,IMenuService
{
  
    private readonly IMapper _mapper;

    public MenuManager(IRepository<Menu> repository, IMapper mapper, IRedisService redisService) : base(repository, mapper, redisService)
    {
        _mapper = mapper;
    }




    // Tüm menüler
    public async Task<List<MenuDto>> GetAllCategories(CancellationToken token)
    {
        var menus = await _repository.GetAll(true);

        var menuDtos = _mapper.Map<List<MenuDto>>(menus);
        foreach (var menuDto in menuDtos)
        {
           
            menuDto.Children = menuDtos
                .Where(m => m.ParentId == menuDto.Id) 
                .ToList();
        }

        // ParentId'si null olan menüleri döndürüyoruz. Bu bir Menünün Ana Menü olduðunu gösterir.
        return menuDtos.Where(m => m.ParentId == null).ToList();
    }





    // Ana menüleri al (ParentId == null)
    public async Task<List<MenuDto>> GetMainCategories(CancellationToken token)
    {
        var menus = await _repository.GetAll(m => m.ParentId == null, false);
        var menuDtos = _mapper.Map<List<MenuDto>>(menus.ToList());
        return menuDtos;
    }

    // Id'ye göre kategori al
    public async Task<List<MenuDto>> GetCategoriesByCategoryId(Guid id)
    {
        var menu = await _repository.GetAsync(m => m.Id == id, false);
        if (menu == null)
            throw new Exception("Menu Bulunamadý");

        var menuDto = _mapper.Map<MenuDto>(menu);
        return new List<MenuDto> { menuDto };
    }


    // Yeni kategori ekle
    public async Task<IDataResult<MenuDto>> AddCategory(MenuDto menuDto)
    {
 
        var menu = _mapper.Map<Menu>(menuDto);
        menu.LanguageId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        menu = BaseEntityHelper.SetBaseEntitiy(menu);
        await _repository.InsertAsync(menu);
        return new SuccessDataResult<MenuDto>(menuDto, "Menü Eklendi");
    }




    // ParentId güncelle
    public async Task<IDataResult<MenuDto>> UpdateParentId(Guid id, Guid? parentId)
    {
        var menu = await _repository.GetAsync(m => m.Id == id, false);
        if (menu == null)
            throw new Exception("Menü Bulunamadý");

        menu.ParentId = parentId ;
        menu = BaseEntityHelper.SetBaseUpdateEntitiy(menu);

        await _repository.UpdateAsync(menu);
        var updatedMenuDto = _mapper.Map<MenuDto>(menu);
        return new SuccessDataResult<MenuDto>(updatedMenuDto, "Parent Güncellendi");
    }

    public async Task<IDataResult<MenuDto>> UpdateCategory(Guid id, MenuDto menuDto)
    {
        var menu = await _repository.GetAsync(m => m.Id == id, false);
        if (menu == null)
            throw new Exception("Menu Bulunamadý");

        menu.Name = menuDto.Name;
        menu.Url = menuDto.Url;
        menu.Icon = menuDto.Icon;
        menu.Description = menuDto.Description;
        menu.DisplayOrder = menuDto.DisplayOrder;
        menu.IsApproved = menuDto.IsApproved;

        menu = BaseEntityHelper.SetBaseUpdateEntitiy(menu);

        await _repository.UpdateAsync(menu);

        var updatedMenuDto = _mapper.Map<MenuDto>(menu);
        return new SuccessDataResult<MenuDto>(updatedMenuDto, "Menu Güncellemesi Baþarýlý");
    }


    // Menü sil
    public async Task DeleteMenu(Guid id)
    {
        var menu = await _repository.GetAsync(m => m.Id == id, false);
        if (menu == null)
            throw new Exception("Menü Bulunamadý");

        await _repository.DeleteAsync(menu);
    }

    // Alt kategori ekle
    public async Task<IDataResult<MenuDto>> AddSubCategory(Guid id, MenuDto menuDto)
    {
     
        var menu = _mapper.Map<Menu>(menuDto);
        menu.ParentId = id;
        menu.LanguageId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        menu = BaseEntityHelper.SetBaseEntitiy(menu);

        await _repository.InsertAsync(menu);
        var addedMenuDto = _mapper.Map<MenuDto>(menu);
        return new SuccessDataResult<MenuDto>(addedMenuDto, "Alt Kategori Eklendi");
    }

}