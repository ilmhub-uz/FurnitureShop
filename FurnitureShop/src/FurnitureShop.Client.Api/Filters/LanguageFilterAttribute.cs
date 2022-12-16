using FurnitureShop.Client.Api.Exeptions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Client.Api.Filters;

public class LanguageFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        RequestCulture.RequestLanguage =
            context.HttpContext.Request.Headers.First(h => h.Key == "language").Value;

        if (RequestCulture.RequestLanguage != "uz" && RequestCulture.RequestLanguage != "en")
        {
            throw new LanguageNotSupportedException();
        }
    }
}