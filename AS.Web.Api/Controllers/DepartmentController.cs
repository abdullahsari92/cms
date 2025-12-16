using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Simple;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class DepartmentController : ControllerBase
    {

        private IDepartmentService _departmentService;
        public DepartmentController( IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var permissionList = await _departmentService.BaseGetAll(token) ?? new ListModel<DepartmentDto>();

            return new SuccessDataResult<ListModel<DepartmentDto>>(permissionList);
          
        }


        [HttpPost]
        public IActionResult Add([FromBody] DepartmentDto departmentDto)
        {
            var permis = _departmentService.BaseInsert(departmentDto);
            return Ok(new SuccessResult("İşlem Başarılı")); ;
        }

        [HttpPost]
        public IActionResult Update([FromBody] DepartmentDto departmentDto)
        {
            var result = _departmentService.BaseUpdate(departmentDto);
            return Ok(new SuccessResult("İşlem Başarılı"));

        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {

            var roleList = await _departmentService.GetSelectOptions();

            return new SuccessDataResult<List<NameValue>>(roleList);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _departmentService.BaseDelete(id);
            return Ok(new SuccessResult("İşlem Başarılı"));

        }
    }
}
