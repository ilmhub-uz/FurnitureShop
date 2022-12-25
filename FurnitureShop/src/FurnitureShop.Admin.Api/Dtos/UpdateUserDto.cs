using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateUserDto
{
    public string? UserName { get; set; }
    public EUserStatus UserStatus { get; internal set; }
}