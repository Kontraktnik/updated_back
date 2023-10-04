using System.IO;
using System.Security.Claims;
using Domain.Models.UserModels;
using Infrastracture.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace API.Middlewares;
public class ProtectFolderOptions
{
    public PathString Path { get; set; }
    public string PolicyName { get; set; }
}
public static class ProtectFolderExtensions
{
    public static IApplicationBuilder UseProtectFolder(
        this IApplicationBuilder builder, 
        ProtectFolderOptions options)
    {
        return builder.UseMiddleware<ProtectFolder>(options);
    }
}

public class ProtectFolder
{
    private readonly RequestDelegate _next;
    private readonly PathString _path;
    private readonly string _policyName;
    
    public ProtectFolder(RequestDelegate next, ProtectFolderOptions options)
    {
        _next = next;
        _path = options.Path;
        _policyName = options.PolicyName;
        
    }
 
    public async Task Invoke(HttpContext httpContext, IAuthorizationService authorizationService)
    {
        if (httpContext.Request.Path.StartsWithSegments(_path))
        {
            string path = Path.GetDirectoryName(httpContext.Request.Path).Split(Path.DirectorySeparatorChar).Last();
            var iin = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (httpContext.User.IsInRole(AppConstant.AdminRoleName) 
                    || httpContext.User.IsInRole(AppConstant.DirectorName)
                    || httpContext.User.IsInRole(AppConstant.ExecutorName)
                    || httpContext.User.IsInRole(AppConstant.KNBRoleName) 
                    ||  httpContext.User.IsInRole(AppConstant.MEDRoleName)
                   )
                {

                    await _next(httpContext);
                }
                else
                {
                    if (path.Length > 1)
                    {
                        Console.WriteLine(path == iin);
                        if (path == iin)
                        {
                            await _next(httpContext);
                        }
                    }
                    await httpContext.ChallengeAsync();
                    return ;
                }
                
            }
            else
            {
                await httpContext.ChallengeAsync();
                return ;
            }
        }
        await _next(httpContext);
 
        
    }
}