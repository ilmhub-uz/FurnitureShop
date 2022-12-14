//using FurnitureShop.Data.Entities;
namespace FurnitureShop.Client.Blazor.ViewModel;

public class OrderView
{
    public Guid Id { get; set; }

    public virtual OrganizationView? Organization { get; set; }

    public virtual UserView? User { get; set; }

    //public EOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProductView>? OrderProducts { get; set; }
}