using Flurl.Util;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Filters
{
    public class IsEntityIdExistsAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsEntityIdExistsAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.Any(dictionary => dictionary.Key.EndsWith("Id")))
            {
                context.Result = new NotFoundResult();
                return;
            }
            var Id = (Guid?)context.ActionArguments.ToList().FirstOrDefault(idictionary => idictionary.Key.EndsWith("Id")).Value;
           
          if (!_unitOfWork.Products.GetAll().Any(pr => pr.Id == Id))
            {
                context.Result = new NotFoundResult();
                return;
            }

            await next();
        }

       
    }
}
