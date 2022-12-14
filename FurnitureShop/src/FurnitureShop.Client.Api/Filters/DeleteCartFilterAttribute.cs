using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Client.Api.Filters;

public class DeleteCartFilterAttribute : ActionFilterAttribute
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteCartFilterAttribute(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("cartProductId"))
        {
            context.Result = new NotFoundResult();
            return;
        }
        var cartProductId = (Guid?)context.ActionArguments["cartProductId"];


        if (!_unitOfWork.Carts.GetAll().Any(cartProduct => cartProduct.Id == cartProductId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next();
    }



}