using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Filters;
public class OrganizationFilterAttribute : TypeFilterAttribute
{
    public OrganizationFilterAttribute(string positions) : base(typeof(OrganizationAttribute))
    {
        Arguments = new object[] { positions };
    }
}