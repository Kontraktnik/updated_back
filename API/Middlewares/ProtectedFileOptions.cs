using Domain;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;
using System.Security.Claims;

namespace API.Middlewares
{
    public static class ProtectedFileOptions
    {
        public static void CheckAuth(StaticFileResponseContext ctx,AppConfig _configuration)
        {
            HttpContext httpContext = ctx.Context;
            if (httpContext.Request.Path.StartsWithSegments($"/{_configuration.UploadRequestStoragePath}"))
            {
                string path = Path.GetDirectoryName(httpContext.Request.Path).Split(Path.DirectorySeparatorChar).Last();
                var iin = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    if (httpContext.User.IsInRole(AppConstant.AdminRoleName)
                        || httpContext.User.IsInRole(AppConstant.DirectorName)
                        || httpContext.User.IsInRole(AppConstant.ExecutorName)
                        || httpContext.User.IsInRole(AppConstant.KNBRoleName)
                        || httpContext.User.IsInRole(AppConstant.MEDRoleName)) { }
                    else
                    {
                        if (path.Length > 1)
                        {

                            if (path == iin)
                            {

                            }
                            else
                            {
                                ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                ctx.Context.Response.ContentLength = 0;
                                ctx.Context.Response.Body = Stream.Null;
                            }
                        }
                        else
                        {
                            ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            ctx.Context.Response.ContentLength = 0;
                            ctx.Context.Response.Body = Stream.Null;
                        }

                    }
                }
                else
                {
                    ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    ctx.Context.Response.ContentLength = 0;
                    ctx.Context.Response.Body = Stream.Null;
                }
            }
        }


    }
}
