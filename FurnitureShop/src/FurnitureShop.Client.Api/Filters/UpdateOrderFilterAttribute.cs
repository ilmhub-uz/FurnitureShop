using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Client.Api.Filters;

public class UpdateOrderFilterAttribute : ActionFilterAttribute
{

    private readonly UnitOfWork _unitOfWork;

    public UpdateOrderFilterAttribute(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("updateOrderDto") && context.ActionArguments.ContainsKey("orderId"))
        {
            await next();
            return;
        }

        await next();
    }
}