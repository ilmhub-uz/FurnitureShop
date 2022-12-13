using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Admin.Api.Filters
{
    public class IsCategoryExistsFilterAttribute : ActionFilterAttribute
    {
        
            private readonly IUnitOfWork _unitOfWork;
            public IsCategoryExistsFilterAttribute(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("categoryId"))
                {
                    context.Result = new NotFoundResult();
                    return;
                }
                var categoryId = (int?)context.ActionArguments["categoryId"];


                if (!_unitOfWork.Categories.GetAll().Any(category => category.Id == categoryId))
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                await next();
            }
    }
}
