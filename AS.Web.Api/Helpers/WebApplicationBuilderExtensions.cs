
using AS.Data;
using AS.Entities.Entity;
using Microsoft.AspNetCore.Identity;


namespace AS.Web.Api.Helpers;

public static class WebApplicationBuilderExtensions
{

    public static IdentityBuilder BuildIdentity(this WebApplicationBuilder webApplicationBuilder)
    {

        return null;
        //return webApplicationBuilder.Services.AddIdentity<UserClient, RoleClient>(
        //    options =>
        //    {
        //        options.Password.RequireDigit = true;
        //        options.Password.RequireLowercase = false;
        //        options.Password.RequireUppercase = false;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.User.RequireUniqueEmail = false;
        //        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        //    }).AddEntityFrameworkStores<EfDbContext>(); ;
    }

}
