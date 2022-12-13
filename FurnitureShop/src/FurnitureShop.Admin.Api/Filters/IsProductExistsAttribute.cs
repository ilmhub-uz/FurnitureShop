using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Filters
{
    public class IsProductExistsAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsProductExistsAttribute(IUnitOfWork unitOfWork)
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
            var productId =(Guid?)context.ActionArguments["productId"];


            if(!_unitOfWork.Products.GetAll().Any(pr => pr.Id == productId))
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }

       
    }
}
