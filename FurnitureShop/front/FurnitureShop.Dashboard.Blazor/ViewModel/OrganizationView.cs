using FurnitureShop.Dashboard.Blazor.Dtos.Enums;

namespace FurnitureShop.Dashboard.Blazor.ViewModel;

public class OrganizationView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public EOrganizationStatus Status { get; set; }
}
