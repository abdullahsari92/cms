using AS.Data;
using AS.Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AS.Web.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ASAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public ASAuthorizeAttribute()
    {

    }
   
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var dbContext = context.HttpContext.RequestServices.GetService<EfDbContext>();

        if (dbContext == null)
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Content = "Not permission"
            };
            return;
        }

        var user = context.HttpContext.User;

        string? apiController = string.Empty;
        string? actionName = string.Empty;

        foreach (var item in context.HttpContext.Request.RouteValues)
        {
            if (item.Key == "controller")
            {
                if (item.Value != null)
                {
                    apiController = item.Value.ToString();
                }
            }
            else if (item.Key == "action")
            {
                if (item.Value != null)
                {
                    actionName = item.Value.ToString();
                }
            }
        }

        bool isAuthorized = false;

        var permissions = dbContext.Set<Permission>().Select(t =>new
        {
            Id = t.Id,
            controllerName = t.ControllerName,
            actionName = t.ActionName
        }).ToList();

        if (permissions.Any(x => x.controllerName == apiController && x.actionName.ToUpper() == actionName.ToUpper()))
        {
            var userRoles = user.Claims.Where(x => x.Type == ClaimTypes.Role);

            if (userRoles.Any())
            {
                foreach (var roleId in userRoles.Select(t => t.Value))
                {
                    var pathPermissions = permissions.Where(x => x.controllerName == apiController && x.actionName.ToUpper() == actionName.ToUpper());

                    foreach (var permission in pathPermissions)
                    {
                        if (dbContext.Set<RolePermissionLine>().Any(x => x.RoleId == new Guid(roleId) && x.PermissionId == permission.Id))
                        {
                            isAuthorized = true;
                            break;
                        }
                        else
                        {
                            isAuthorized = false;
                        }
                    }

                    if (isAuthorized)
                    {
                        break;
                    }
                }
            }
            else
            {
                isAuthorized = false;
            }
        }
        else
        {
            isAuthorized = true;
        }
        Thread.CurrentPrincipal = new ClaimsPrincipal(user);

        if (!isAuthorized)
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Content = "Not permission"

            };

            return;
        }
    }
}
