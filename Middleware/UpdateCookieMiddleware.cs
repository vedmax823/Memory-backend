using System;

namespace Memory.Middleware;

public class UpdateCookieMiddleware
{
    private readonly RequestDelegate _next;

    public UpdateCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("userName", out string? userName))
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7) // Оновлюємо термін дії на 7 днів
            };

            context.Response.Cookies.Append("userName", userName, cookieOptions);
        }

        await _next(context);
    }
}
