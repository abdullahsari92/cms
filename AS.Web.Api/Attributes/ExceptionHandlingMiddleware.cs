using AS.Business.Interfaces;
using AS.Core.Exceptions;
using AS.Core.Helpers;
using AS.Core.ValueObjects;
using AS.Data;
using AS.Entities.Dtos;
using AS.Entities.Entity;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IDS.WebApi.Infrastructure
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }      
            catch (NotPermissionException exception)
            {
                context.Response.StatusCode =
             StatusCodes.Status401Unauthorized;

                var problemDetails = new ErrorResult(exception.Message);

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception exception)
            {
                if (exception is DuplicateException || exception is CustomException || exception is ValidationException)
                {

                    var nameEx = nameof(CustomException);
                    context.Response.StatusCode =StatusCodes.Status418ImATeapot;

                    var results = new ErrorResult(exception.Message, nameEx);

                    await context.Response.WriteAsJsonAsync(results);
                    return;
                }
                else if(exception.Message == "The operation was canceled.")
                {
                    await _next(context);
                }
                else
                {

                    var dbContext = context.RequestServices.GetService<EfDbContext>();

                    string? apiController = string.Empty;
                    string? actionName = string.Empty;

                    foreach (var item in context.Request.RouteValues)
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

                    var log = new LogInfo()
                    {
                        Message = exception.Message,
                        Template= exception?.InnerException?.Message ?? "null",                        
                        ControllerActionName= apiController + "/" + actionName,
                        TransactionerTime= DateTime.UtcNow,
                        TransactionerUserId= UserInfoExtensions.GetUserId()
                   };
                      dbContext.Set<LogInfo>().Add(log);
                    await dbContext.SaveChangesAsync();

                    context.Response.StatusCode =    StatusCodes.Status400BadRequest;

                }
                
                var problemDetails = new ErrorResult(exception.Message);

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
