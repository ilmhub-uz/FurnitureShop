using FurnitureShop.Admin.Api.Enums;
using FurnitureShop.Admin.Api.Dtos.Enums;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class ProductFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public int? CategoryId { get; set; }
    public uint? Price { get; set; }
    public string? Brend { get; set; }
    public DateTime? DateTime { get; set; }
    public EProductSorting? ProductSorting { get; set; }
    public EProductStatus? Status {get;set;}
    public EProductSorting? SortingName { get; set; }
}