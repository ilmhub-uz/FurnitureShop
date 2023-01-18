namespace FurnitureShop.Merchant.Blazor.ViewModel;

public class OrganizationView
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public EOrganizationStatus Status { get; set; }
    public List<ProductView>? Products { get; set; }
    public List<OrganizationUserView>? Users { get; set; }
    public string OwnerName { get; set; }
}