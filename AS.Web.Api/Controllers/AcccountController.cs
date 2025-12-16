using AS.Business;
using AS.Business.Interfaces;
using AS.Core.ValueObjects;
using AS.Entities.Dtos;
using AS.Entities.Models;
using AS.Web.Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AS.Web.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [ASAuthorize]
    [ApiExplorerSettings(GroupName = "is-Private")]
    public class AccountController : ControllerBase
    {

        private IAccountService _accountManager;
        private readonly IAuthService _authService;

        public AccountController(IAccountService accountManager, IAuthService authService)
        {
            _accountManager = accountManager;
            _authService = authService;
        }



        [HttpGet]
        public async Task<ActionResult<Core.IResult>> GetProfile(CancellationToken cancellationToken)
        {

            PersonDto model = new PersonDto();

            model = await _accountManager.GetProfile(cancellationToken);

            return new SuccessDataResult<PersonDto>(model);
        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> Login([FromBody] LoginModel model)
        {
            AuthModel authModel = await _authService.Login(model, false);
            return Ok(new SuccessDataResult<AuthModel>(authModel, "Success"));

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> ChangePassword([FromBody] PasswordChangeModel passwordChangeModel)
        {

            await _accountManager.PasswordChange(passwordChangeModel);

            return new SuccessDataResult<bool>(true);

        }

        [HttpPost]
        public async Task<ActionResult<Core.IResult>> UpdateProfile([FromBody] PersonDto personDto)
        {

            personDto = await _accountManager.UpdateProfile(personDto);

            return new SuccessDataResult<PersonDto>(personDto);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.IResult>> GetRolesDepartments(Guid Id)
        {

            PersonDto model = new PersonDto();

            model = await _accountManager.GetById(Id);

            return new SuccessDataResult<PersonDto>(model);
        }

      
    }
}
