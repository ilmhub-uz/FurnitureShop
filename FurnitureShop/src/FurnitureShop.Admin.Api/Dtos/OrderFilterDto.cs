using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class OrderFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
}