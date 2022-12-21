using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos.FilterDtos;

public class OrganizationFilterDto : PaginationParams
{
    public Guid? UserId { get; set; }
}
