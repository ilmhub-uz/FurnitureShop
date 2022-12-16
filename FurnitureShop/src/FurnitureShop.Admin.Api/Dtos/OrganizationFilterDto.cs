using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos;

public class OrganizationFilterDto : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public Guid? UserId { get; set; }
}
