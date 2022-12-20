using FurnitureShop.Admin.Api.Dtos.Enums;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class ProductFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public int? CategoryId { get; set; }
    public EProductStatus? Status {get;set;}
    public EProductSorting? SortingName { get; set; }
}