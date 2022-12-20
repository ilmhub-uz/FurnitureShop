using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class ProductFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public int? CategoryId { get; set; }
    public EProductSorting ProductSorting { get; set; }
}