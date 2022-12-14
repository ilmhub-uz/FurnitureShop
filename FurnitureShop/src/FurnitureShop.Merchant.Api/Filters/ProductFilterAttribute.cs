using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Filters;

public class ProductFilterAttribute : TypeFilterAttribute
{
    public ProductFilterAttribute(string positions) : base(typeof(ProductAttribute))
    {
        Arguments = new object[] { positions };
    }
}