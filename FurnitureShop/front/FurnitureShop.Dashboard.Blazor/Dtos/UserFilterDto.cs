using FurnitureShop.Dashboard.Blazor.Dtos.Enums;
using FurnitureShop.Dashboard.Blazor.Models;

namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class UserFilterDto : PaginationParams
{
    public EUserStatus UserStatus { get; set; }
}
