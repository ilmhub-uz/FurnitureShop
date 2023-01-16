

using FurnitureShop.Admin.Blazor.Dtos.Enums;

namespace FurnitureShop.Admin.Blazor.ViewModel;

public class UserView
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public EUserStatus Status { get; set; }
    public string? AvatarUrl { get; set; }
}