using AS.Business;
using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Simple;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class SettingController : ControllerBase
    {

        private ISettingService _settingService;
        public SettingController( ISettingService settingService)
        {
            _settingService = settingService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            
            var permissionList = await _settingService.BaseGetAll(token) ?? new ListModel<SettingDto>();

            return new SuccessDataResult<ListModel<SettingDto>>(permissionList);
          
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Core.IResult>> GetByCode(string code)
        {
        
                var model = await _settingService.GetByCode(code);
                return new SuccessDataResult<SettingDto>(model);      

        }



        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] SettingDto settingDto)
        {
                        
               var permis = await  _settingService.BaseInsert(settingDto);                 

            return Ok(new SuccessDataResult<SettingDto>(permis,"işlem başarılı"));
        }



        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] SettingDto settingDto)
        {      
           
                var result = await _settingService.BaseUpdate(settingDto);

                return Ok(new SuccessDataResult<SettingDto>(result));
        
        }




        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {

            var roleList = await _settingService.BaseGetSelectOptions();

            return new SuccessDataResult<List<NameValue>>(roleList);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid id)
        {
          
                await  _settingService.BaseDelete(id);         
              return Ok(new SuccessResult("İşlem Başaralı"));

        }
    }
}
