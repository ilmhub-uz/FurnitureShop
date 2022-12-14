using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Filters;
public class ProductAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;
    public string[] Positions { get; set; }
    public ProductAttribute(AppDbContext context, string positions)
    {
        _context = context;
        Positions = positions.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Guid? organizationId = default;

        if (!(context.ActionArguments.ContainsKey("productId")))
        {
            await next();
            return;
        }
        else if (context.ActionArguments.ContainsKey("productId"))
        {
            var productId = (Guid?)context.ActionArguments["productId"];



            var product = await _context.Products.FirstOrDefaultAsync(pro => pro.Id == productId);
            if (product is null)
            {
                context!.Result = new NotFoundResult();
                return;
            }
            organizationId = product.OrganizationId;
        }


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