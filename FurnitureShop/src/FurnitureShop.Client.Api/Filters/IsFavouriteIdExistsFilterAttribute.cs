using FurnitureShop.Data.Context;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Filters;

public class IsFavouriteIdExistsFilterAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;
    public IsFavouriteIdExistsFilterAttribute(AppDbContext context)
    {
        _context = context;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("favouriteId"))
        {
            context.Result = new NotFoundResult();
            return;
        }
        var favouriteId = (int?)context.ActionArguments["favouriteId"];


        if (!_context.FavouriteProducts.FirstOrDefault(favourite => favourite.ProductId == favouriteId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next();
    }
}