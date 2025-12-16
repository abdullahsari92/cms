using AS.Business;
using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class PermissionController : ControllerBase
    {

        private IPermissionService _permissionService;
        private IRoleService _roleService;
        public PermissionController(IPermissionService permissionService, IRoleService roleService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {
            var permissionList = await _permissionService.BaseGetAll(token) ?? new ListModel<PermissionDto>();
            return new SuccessDataResult<ListModel<PermissionDto>>(permissionList);
        }


        [HttpPost]
        public IActionResult Add([FromBody] PermissionDto permissionDto)
        {
            var permis = _permissionService.Insert(permissionDto);
            return Ok(new SuccessResult("İşlem Başaralı"));
        }



        [HttpPost]
        public IActionResult Update([FromBody] PermissionDto permissionDto)
        {

            var dger = _permissionService.BaseUpdate(permissionDto);

            return Ok(new SuccessResult("İşlem Başaralı"));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _permissionService.BaseDelete(id);

            return Ok(new SuccessDataResult<bool>(true));

        }
    }
}
