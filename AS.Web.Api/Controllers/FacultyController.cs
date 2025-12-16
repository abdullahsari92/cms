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
    public class FacultyController : ControllerBase
    {

        private IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {


            var permissionList = await _facultyService.BaseGetAll(token) ?? new ListModel<FacultyDto>();

            return new SuccessDataResult<ListModel<FacultyDto>>(permissionList);

        }


        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] FacultyDto facultyDto)
        {


            await _facultyService.BaseInsert(facultyDto);

            return Ok(new SuccessResult("İşlem Başaralı"));
        }



        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] FacultyDto facultyDto)
        {

            await _facultyService.BaseUpdate(facultyDto);

            return Ok(new SuccessResult("İşlem Başaralı"));
        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {

            var roleList = await _facultyService.GetSelectOptions();
            return new SuccessDataResult<List<NameValue>>(roleList);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            _facultyService.BaseDelete(id);
            return Ok(new SuccessResult("İşlem Başaralı"));

        }
    }
}
