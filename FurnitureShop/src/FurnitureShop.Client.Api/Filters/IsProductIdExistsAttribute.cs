using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Client.Api.Filters;

public class IsProductIdExistsAttribute : ActionFilterAttribute
{
    private readonly IUnitOfWork _unitOfWork;
    public IsProductIdExistsAttribute(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("productId"))
        {
            context.Result = new NotFoundResult();
            return;
        }
        var productId = (Guid?)context.ActionArguments["productId"];


        if (!_unitOfWork.Products.GetAll().Any(pr => pr.Id == productId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next();
    }
}