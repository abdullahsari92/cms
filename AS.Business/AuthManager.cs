using AS.Business.EmailMessage;
using AS.Business.Interfaces;
using AS.Core;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.Security;
using AS.Core.ValueObjects;
using AS.Entities.Entity;
using AS.Entities.Enums;
using AS.Entities.Models;
using AutoMapper;
using Core.Utilities.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AS.Business
{
    public class AuthManager : IAuthService
    {
        public readonly IUserService _userService;
        protected readonly IRepository<User> _repositoryUser;
        protected readonly IRepository<Role> _repositoryRole;

        private IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        public readonly IEMailSender _emailSender;


        private readonly IRoleService _roleService;
        public AuthManager(IUserService userService = null, IRepository<User> repositoryUser = null
           , IMapper mapper = null, IOptions<JwtSettings> jwtSettings = null, IRoleService roleService = null, IEMailSender emailSender = null, IRepository<Role> repositoryRole = null)
        {
            _userService = userService;
            _repositoryUser = repositoryUser;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _roleService = roleService;
            _emailSender = emailSender;
            _repositoryRole = repositoryRole;
        }

        public async Task<AuthModel> Login(LoginModel model, bool isPasswordUse = true)
        {
            var query = await _repositoryUser.GetAll();

            var user = query.FirstOrDefault(p => p.Email == model.Email.Trim());

            if (user == null)
            {
                throw new NotFoundException();
            }

            // �ifresi yanl�� ise
            if (isPasswordUse && HashingHelper.VerifyPasswordHash(model.Password.Trim()) != user.Password.Trim())
            {
                throw new ValidationException("password uygun de�il");
            }


            if (!user.IsApproved)
            {
                throw new NotApprovedException();
            }
            var claims = await GetClaims(model);
            var token = await GenerateJwt(claims);
            var userRoles = claims.Where(x => x.Type == ClaimTypes.Role).Select(p => new Guid(p.Value)).ToList(); //�imdilik tek role olarak hesapland�


            var getPermissionClaims = await _roleService.GetPermissionClaims(userRoles);

            var roleLevel = claims.Where(x => x.Type == "roleLevel").FirstOrDefault().Value;
            var queryRole = await  _repositoryRole.GetAll(p => p.Level == Convert.ToInt32(roleLevel));

            Role activeMaxRole =await queryRole.Include(x => x.RoleUserLines.Where(m=>m.UserId == user.Id)).ThenInclude(p=>p.Units).FirstOrDefaultAsync();

           bool isFacultyRole = activeMaxRole?.RoleUserLines?.FirstOrDefault()?.Role?.Level == (int)RoleLevel.FakulteSorumlusu;

            AuthModel authModel = new()
            {
                Claims = getPermissionClaims,
                Token = token,
                Email = user.Email,
                FullName = user.Username,
                UserType = user.UserType,
                UnitId = model.UnitId,
                RoleName = activeMaxRole.Name,
                DeparmentName = isFacultyRole? "" : activeMaxRole?.RoleUserLines?.Where(p=>p.UnitId == model.UnitId)?.FirstOrDefault()?.Units?.Name,
                FacultyName = activeMaxRole?.RoleUserLines?.Where(p => p.UnitId == model?.UnitId)?.FirstOrDefault()?.Units?.Name,
            };


            //claims.AddRange(roleClaims);

            return authModel;

        }


        public async Task<IDataResult<bool>> ForgotPassword(string email)
        {
            var query = await _repositoryUser.GetAll();

            var user = query.FirstOrDefault(p => p.Email == email.Trim());


            if (user == null)
            {
                throw new NotFoundException();
            }
            if (!user.IsApproved)
            {
                throw new NotApprovedException();
            }

            var randomPassword = SecurityHelper.CreatePassword(8);


            user.Password = HashingHelper.VerifyPasswordHash(randomPassword);


            bool result = _emailSender.Sender(user.Email, randomPassword);

            if (!result)
            {
                return new ErrorDataResult<bool>(false, "Mail Hatas�");

            }
            await _repositoryUser.UpdateAsync(user);


            return new SuccessDataResult<bool>(true, "i�lem ba�ar�l�");

        }

        public async Task<List<Claim>> GetClaims(LoginModel model)
        {
            var query = await _repositoryUser.GetAll();

            var user = query.Include(m => m.RoleUserLines).ThenInclude(r => r.Role)
                            .Include(m => m.RoleUserLines).ThenInclude(r => r.Units)
                            .Include(p => p.Person)
                            .FirstOrDefault(p => p.Email == model.Email);


            if (user == null)
            {
                throw new NotFoundException();
            }

            List<Claim> claims = new()
                {
                    new("UserId", user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email),
                    new("UserType", user.UserType.ToString()),

            };
                      

            //var roleClaims = (await _userManager.GetRolesAsync(userClient)).Select(r => new Claim(ClaimTypes.Role, r));

            List<Role> roles =  new List<Role>();
            if(model.UnitId != null)
            {
              
                var unitId = model.UnitId ?? new Guid();
                roles = user.RoleUserLines.Where(p => p.UnitId == unitId).Select(m => m.Role).ToList();
            }else
            {
                 roles = user.RoleUserLines.Where(p => p.UnitId == null).Select(m => m.Role).ToList();

            }

            var roleLevel = roles.Min(p => p.Level);

            if (roleLevel != null)
            {
                claims.Add(new("roleLevel", roleLevel.ToString()));
            }
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Id.ToString()));
            }

            //claims.AddRange(roleClaims);

            return claims;
        }

        private async Task<string> GenerateJwt(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var token = new JwtSecurityToken(
            _jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.ExpirationInMinutes)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
