using Microsoft.AspNetCore.Http;
using Quiz.API.Static;
using Quiz.Core;
using Quiz.Data.Model.System.Authorization;
using Quiz.Data.Service.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.API.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAuthorizationService authorizationService)
        {
            try
            {
                string token = httpContext.Request.Headers["token"].ToString();
                string controller = httpContext.Request.RouteValues["controller"]?.ToString().ToLower();
                string action = httpContext.Request.RouteValues["action"]?.ToString().ToLower();
                if (controller != "auth")
                {
                    if (!string.IsNullOrEmpty(token))
                    {
                        Authorize result = authorizationService.Get(token);
                        if (result != null)
                        {
                            Current.User = result.User;
                            if (result.AuthorizedActions.Any(c => c.ControllerName.Equals(controller) && c.ActionName.Equals(action)))
                            {
                                await _next.Invoke(httpContext);
                            }
                            else
                            {
                                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                                httpContext.Response.ContentType = "application/json";
                                await httpContext.Response.WriteAsync(new Result<object>(false, "You are not authorized").ToJson());
                                return;
                            }
                        }
                        else
                        {
                            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            httpContext.Response.ContentType = "application/json";
                            await httpContext.Response.WriteAsync(new Result<object>(false, "Invalid token").ToJson());
                            return;
                        }
                    }
                    else
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsync(new Result<object>(false, "Invalid token").ToJson());
                        return;
                    }
                }
                else
                {
                    await _next.Invoke(httpContext);
                }
            }
            catch
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(new Result<object>(false, "On error occurred").ToJson());
                return;
            }
        }
    }
}
