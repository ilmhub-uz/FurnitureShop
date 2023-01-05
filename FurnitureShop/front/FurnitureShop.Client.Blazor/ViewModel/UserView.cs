//using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Blazor.ViewModel;

public class UserView
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Status { get; set; }
    public string? AvatarUrl { get; set; }
}