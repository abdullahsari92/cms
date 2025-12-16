using AS.Business.Interfaces;
using AS.Core.Helpers;
using AS.Entities.Dtos;


namespace IDS.WebApi.Infrastructure
{

    // Her request öncesi çalışır.
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var httpContext = context.Request.HttpContext;        
            try
            {    
            
                var pathValue = httpContext.Request.Path.Value;
                if (pathValue != "/" && httpContext.Request.Method != "OPTIONS")
                {
                    pathValue = pathValue.Replace("api/", "");

                    var pathValueArray = pathValue.Split('/');

                    if (pathValueArray.Length > 1)
                    {
                        string controller;
                        string action;

                        if (pathValueArray.Length == 2)
                        {
                            controller = pathValueArray[1];
                            action = "Index";
                        }
                        else
                        {
                            controller = pathValueArray[1];
                            action = pathValueArray[2];
                        }

                        #region burası sadece controller ve action permission tablosuna eklemek için
                        var permisson = httpContext.RequestServices.GetService<IPermissionService>();

                        permisson.Insert(new PermissionDto()
                        {
                            ActionName = action.ToTitleCase(),
                            ControllerName = controller,
                            Name = controller.ToUpper() + "_" + action.ToUpper(),
                            Description = "",

                        }); ;

                        #endregion


                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }



                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }





            await _next(context);
        }
    }
}
