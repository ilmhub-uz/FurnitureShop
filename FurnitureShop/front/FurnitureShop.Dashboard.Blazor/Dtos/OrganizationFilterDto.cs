using FurnitureShop.Dashboard.Blazor.Models;

namespace FurnitureShop.Dashboard.Blazor.Dtos;



public class OrganizationFilterDto : PaginationParams
{
    public Guid? UserId { get; set; }
}
