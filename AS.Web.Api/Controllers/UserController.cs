using AS.Business;
using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Web.Api.Attributes;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class UserController : ControllerBase
    {

        private IPersonService _personService;

        public UserController(IPersonService personService)
        {

            _personService = personService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var userListbase = await _personService.GetAll(token);


            return new SuccessDataResult<ListModel<PersonDto>>(userListbase);

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid Id)
        {

            var model = await _personService.GetById(Id);

            return Ok(new SuccessDataResult<PersonDto>(model));
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] PersonDto personDto)
        {
            var result = await _personService.Insert(personDto);
            return Ok(new SuccessDataResult<PersonDto>(result.Data));
        }



        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] PersonDto personDto)
        {
            await _personService.Update(personDto);
            return Ok(new SuccessDataResult<bool>(true));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _personService.Delete(id);

            return Ok(new SuccessDataResult<bool>(true));
        }
    }
}
