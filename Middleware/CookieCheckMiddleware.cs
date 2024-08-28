using System;

namespace Memory.Middleware;

public class CookieCheckMiddleware
{
    private readonly RequestDelegate _next;

    public CookieCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();

        if (path.StartsWith("/api/user/register"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Cookies.ContainsKey("userName"))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        await _next(context);
    }
}
