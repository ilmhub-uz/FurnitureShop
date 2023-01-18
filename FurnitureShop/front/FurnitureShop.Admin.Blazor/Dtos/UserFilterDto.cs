using FurnitureShop.Admin.Blazor.Dtos.Enums;
using FurnitureShop.Admin.Blazor.Models;


namespace FurnitureShop.Admin.Blazor.Dtos;

public class UserFilterDto : PaginationParams
{
    public EUserStatus UserStatus { get; set; }
}
