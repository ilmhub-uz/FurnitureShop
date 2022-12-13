using FurnitureShop.Data.Entities;

namespace FurnitureShop.Api.ViewModel;

public class OrganizationView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public EOrganizationStatus Status { get; set; }
}