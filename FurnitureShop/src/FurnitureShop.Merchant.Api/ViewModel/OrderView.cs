using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.ViewModel;

public class OrderView
{
    public Guid Id { get; set; }
    public string? OrganizationName { get; set; }
    public string? UserName { get; set; }
    public string? UserMail { get; set; }
    public EOrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public virtual ICollection<OrderProductView>? OrderProducts { get; set; }
}
