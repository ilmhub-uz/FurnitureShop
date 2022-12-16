using Microsoft.AspNetCore.Localization;

namespace FurnitureShop.Client.Api.Middlewares;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next) =>
        _next = next;

    public Task Invoke(HttpContext httpContext)
    {

        if (!httpContext.Request.Headers.ContainsKey("Language"))
        {
            throw new Exception("Language header missed!");
        }

        RequestCulture.RequestLanguage = httpContext.Request.Headers["Language"];

        return _next(httpContext);

    }
}