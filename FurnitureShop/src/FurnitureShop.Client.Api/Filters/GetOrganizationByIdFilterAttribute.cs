using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Filters;

public class GetOrganizationByIdFilterAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public GetOrganizationByIdFilterAttribute(AppDbContext context)
    {
        _context = context;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.ContainsKey("organizationId"))
        {
            await next();
            return;
        }

        var organizationId = (Guid?)context.ActionArguments["organizationId"];

        if (!await _context.Organizations.AnyAsync(course => course.Id == organizationId))
        {
            context.Result = new NotFoundResult();
            return;
        }

        await next();
    }
}