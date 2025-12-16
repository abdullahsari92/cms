using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AS.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class UserInfoExtensions
    {
        private static readonly IHttpContextAccessor HttpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        public static Guid GetUserId()
        {          

            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return new Guid();
            }
            else
            {
                return user != null && user.Identity.IsAuthenticated ? new Guid(user?.FindFirst("UserId")?.Value) : new Guid();
            }
        }

        public static List<string> GetClaims()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return new();
            }
            else
            {
                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return new();
                }
                //var deger = user.Claims.Where(x => x.Type == ClaimTypes.Role);

                return null;
            }
        }

        public static bool HasPermission(string permission)
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return false;
            }
            else
            {
                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                {
                    return false;
                }
                return user.IsInRole(permission);
            }
        }

        public static Guid? GetUnitId()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            else
            {

                var fixedUnitId = new Guid("e0f90674-f6b6-4370-8f0e-1fb93124d115");
                return fixedUnitId;
            }
        }
        public static int? GetRoleLevel()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var dd = user?.FindFirst("roleLevel") ?? null;
                    if (dd == null) return null; ;
                    return Convert.ToInt32(user?.FindFirst("roleLevel")?.Value);
                }


                return null;
            }
        }
 
        public static Guid? GetStudentId()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var dd = user?.FindFirst("StudentId") ?? null;
                    if (dd == null) return null; ;
                    return new Guid(user?.FindFirst("StudentId")?.Value);
                }


                return null;
            }
        }
        public static Guid? GetFacultyId()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var dd = user?.FindFirst("FacultyId") ?? null;
                    if (dd == null) return null; ;
                    return new Guid(user?.FindFirst("FacultyId")?.Value);
                }


                return null;
            }
        }
        public static int? GetUserType()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var dd = user?.FindFirst("UserType") ?? null;
                    if (dd == null) return null; ;
                    return Convert.ToInt32(user?.FindFirst("UserType")?.Value);
                }


                return null;
            }
        }

        public static String UrlIsLive()
        {
            try
            {
                var host = HttpContextAccessor.HttpContext.Request.Host.Host;
                if (host.StartsWith("api"))
                {
                    return "";
                }
                return "test_";
            }
            catch (System.Exception)
            {

                return "";
            }

        }
    }
}

