using AS.Business;
using AS.Business.Interfaces;
using AS.Core.Security;
using AS.Core.ValueObjects;
using AS.Entities.Models;
using AS.Entities.Simple;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AS.Web.Api.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
[ApiExplorerSettings(GroupName = "is-Private")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;
    private IAccountService _accountManager;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings, IAccountService accountManager)
    {
        _authService = authService;
        _jwtSettings = jwtSettings.Value;
        _accountManager = accountManager;
    }


    [HttpPost]
    public async Task<ActionResult<Core.IResult>> Login([FromBody] LoginModel model)
    {
            AuthModel authModel = await _authService.Login(model);
            return Ok(new SuccessDataResult<AuthModel>(authModel, "Success"));

    }

    [HttpPost]
    public async Task<ActionResult<Core.IResult>> ForgotPassword([FromBody] LoginModel model)
    {
        var forgotPasword = await _authService.ForgotPassword(model.Email);

        return Ok(forgotPasword);

    }

    [HttpGet]
    public async Task<ActionResult<Core.IResult>> GetSelectOptionsDepartmentsByEmail(string email)
    {

        var studentList = await _accountManager.GetSelectOptionsDepartmans(email);

        return new SuccessDataResult<List<NameValue>>(studentList);

    }

}
