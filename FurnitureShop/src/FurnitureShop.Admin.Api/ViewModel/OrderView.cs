using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.ViewModel;

public class OrderView
{
    public Guid Id { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual AppUser? User { get; set; }

    public EOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
}
