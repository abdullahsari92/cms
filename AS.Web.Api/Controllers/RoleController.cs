using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Models;
using AS.Entities.Simple;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    //[ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]

    public class RoleController : ControllerBase
    {

        private IRoleService _roleManager;
        public RoleController(IRoleService roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List()
        {

            var roleList = await _roleManager.GetAll();

            return new SuccessDataResult<ListModel<RoleDto>>(roleList);

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid Id, CancellationToken cancellationToken)
        {

            RoleDetailModel model  = new RoleDetailModel();

            model = await _roleManager.Get(Id, cancellationToken);

            return new SuccessDataResult<RoleDetailModel>(model);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] RoleDto RoleDto)
        {
                 RoleDto = await _roleManager.BaseInsert(RoleDto);
                return new SuccessDataResult<RoleDto>(RoleDto);

        }


        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] RoleDetailModel roleUpdateModel)
        {
        
                roleUpdateModel.PermissionList = await _roleManager.RolePermissionAdd(roleUpdateModel);
                roleUpdateModel.RoleDto = await _roleManager.BaseUpdate(roleUpdateModel.RoleDto);
                return new SuccessDataResult<RoleDetailModel>(roleUpdateModel);         

        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {

            var roleList = await _roleManager.GetSelectOptions();

            return new SuccessDataResult<List<NameValue>>(roleList);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid Id)
        {

          
                //todo: role permissiline silip silinecek
               await _roleManager.BaseDelete(Id);
  
            return Ok(true);
        }
    }
}
