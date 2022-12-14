using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace FurnitureShop.Merchant.Api.Filters;
public class OrganizationAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;
    public string[]? Positions { get; set; }
    public OrganizationAttribute(AppDbContext context, string? positions)
    {
        _context = context;
        Positions = positions?.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!(context.ActionArguments.ContainsKey("organizationId")))
        {
            await next();
            return;
        }

        var organizationId = (Guid?)context.ActionArguments["organizationId"];



        var user = context.HttpContext.User;

        var _userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(_userId, out Guid userId);


        var organizationUsers = await _context.OrganizationUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.OrganizationId == organizationId);

        if (organizationUsers is null)
        {
            context.Result = new NotFoundResult();
            return;
        }

        if ((Positions?.Any(postion => postion.Contains("Owner"))) is true)
        {
            if (organizationUsers.Role != FurnitureShop.Data.Entities.ERole.Owner)
            {
                context.Result = new NotFoundResult();
                return;
            }
        }
        if ((Positions?.Any(postion => postion.Contains("Manager")) is true))
        {
            if (organizationUsers.Role != FurnitureShop.Data.Entities.ERole.Manager)
            {
                context.Result = new NotFoundResult();
                return;
            }
        }
        if ((Positions?.Any(postion => postion.Contains("Seller")) is true))
        {
            if (organizationUsers.Role != FurnitureShop.Data.Entities.ERole.Seller)
            {
                context.Result = new NotFoundResult();
                return;
            }
        }

        await next();

    }
}