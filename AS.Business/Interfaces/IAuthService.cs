using AS.Business.Interfaces;
using AS.Core;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using AS.Entities.Models;
using System.Security.Claims;

namespace AS.Business
{
    public interface IAuthService
    {
        Task<AuthModel> Login(LoginModel model, bool isPasswordUse = true);

        Task<List<Claim>> GetClaims(LoginModel model);


        Task<IDataResult<bool>> ForgotPassword(string email);

    }
}