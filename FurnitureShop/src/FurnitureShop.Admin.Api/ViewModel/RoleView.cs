using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.ViewModel;

public class RoleView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<string>? Permissions { get; set; }
}