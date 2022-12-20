using FurnitureShop.Common.Models;
using FurnitureShop.Merchant.Api.Helpers;

namespace FurnitureShop.Merchant.Api.Dtos;

public class ProductFilterDto : ProductPaganitionParams
{
    public Guid? OrganizationId { get; set; }
    public int? CategoryId { get; set; }
}

