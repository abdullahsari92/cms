using AS.Business;
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
    [ASAuthorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class PersonController : ControllerBase
    {

        private IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet]
        public async Task<ActionResult<Core.IResult>> List(CancellationToken token)
        {

            var personList = await _personService.BaseGetAll(token);

            return new SuccessDataResult<ListModel<PersonDto>>(personList);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetById(Guid Id)
        {

            PersonDto model = new PersonDto();

            model = await _personService.GetById(Id);

            return new SuccessDataResult<PersonDto>(model);
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Add([FromBody] PersonDto personDto)
        {


            var result = await _personService.Insert(personDto);

            return new SuccessDataResult<PersonDto>(result.Data);
        }



        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Update([FromBody] RoleDetailModel studentUpdateModel)
        {


            //  studentUpdateModel.PersonDto = await _personService.BaseUpdate(studentUpdateModel.PersonDto);

            return new SuccessDataResult<RoleDetailModel>(studentUpdateModel);

        }

        [HttpGet]
        public async Task<ActionResult<Core.IResult>> SelectOption()
        {

            var personList = await _personService.BaseGetSelectOptions();

            return new SuccessDataResult<List<NameValue>>(personList);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Core.IResult>> Delete(Guid Id)
        {
            await _personService.BaseDelete(Id);

            return Ok(new SuccessResult("İşlem Başarılı"));
        }
    }
}
