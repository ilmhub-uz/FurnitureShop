using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class ProductFilterDto : PaginationParams
{
    public Guid OrganizationId { get; set; }
    public Guid CategoryId { get; set; }
}