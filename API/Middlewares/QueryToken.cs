using Microsoft.Net.Http.Headers;

namespace API.Middlewares;

public class QueryToken
{
    private readonly RequestDelegate _next;

    public QueryToken(RequestDelegate next) => _next = next;

    public Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Query.TryGetValue("token", out var token))
        {
            context.Request.Headers[HeaderNames.Authorization] = $"Bearer {token}";
        }

        return _next(context);
    }
}