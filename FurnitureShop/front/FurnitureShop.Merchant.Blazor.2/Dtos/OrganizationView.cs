namespace FurnitureShop.Merchant.Blazor.Dtos;

public class OrganizationView
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public int Status { get; set; }
    /*public virtual ICollection<OrganizationUser>? Users { get; set; }
    public virtual ICollection<Product>? Products { get; set; }*/
}
