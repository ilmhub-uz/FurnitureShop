using FurnitureShop.Admin.Blazor.Dtos.Enums;


namespace FurnitureShop.Admin.Blazor.ViewModel;

public class OrganizationView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public EOrganizationStatus Status { get; set; }
}
