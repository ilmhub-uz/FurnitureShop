﻿using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Filters;

public class IsOrganizationIdExistsFilterAttribute : ActionFilterAttribute
{
    private readonly IUnitOfWork _unitOfWork;
    public IsOrganizationIdExistsFilterAttribute(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("organizationId"))
        {
            context.Result = new NotFoundResult();
            return;
        }
        var organizationId = (Guid?)context.ActionArguments["organizationId"];


        if (!_unitOfWork.Organizations.GetAll().Any(organization => organization.Id == organizationId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next();
    }


}