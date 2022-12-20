using FurnitureShop.Common.Models;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Helpers;
public class ProductPaganitionParams : PaginationParams
{
    public string? ProductName { get; set; }
    public bool Price { get; set; }
    public bool Views { get; set; }
    public bool ReverseOrder { get; set; }
    public PriceAmounts? PriceAmounts { get; set; }
}