using FurnitureShop.Admin.Blazor.Models;

namespace FurnitureShop.Admin.Blazor.Dtos;



public class OrganizationFilterDto : PaginationParams
{
    public Guid? UserId { get; set; }
}
