using FurnitureShop.Dashboard.Blazor.Dtos.Enums;
using FurnitureShop.Dashboard.Blazor.Models;

namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class ProductFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public int? CategoryId { get; set; }
    public uint? Price { get; set; }
    public string? Brend { get; set; }
    public uint? Rate { get; set; }
    public DateTime? DateTime { get; set; }
    public EProductSorting? ProductSorting { get; set; }
    public EProductStatus? Status { get; set; }
    public EProductSorting? SortingName { get; set; }
}