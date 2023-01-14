using System.ComponentModel.DataAnnotations;
using FurnitureShop.Common.Models;
using FurnitureShop.Merchant.Api.Dtos.Enums;

namespace FurnitureShop.Merchant.Api.Dtos;
public class ProductSortingFilter : PaginationParams
{
    public bool? OnlyMyProducts { get; set; }
    [Required]
    public Guid OrganizationId { get; set; }
    public decimal? Price { get; set; }
    public ESortingParameters? SortingParams { get; set; }
    public string? Brend { get; set; }
    public int? CategoryId { get; set; }
}
