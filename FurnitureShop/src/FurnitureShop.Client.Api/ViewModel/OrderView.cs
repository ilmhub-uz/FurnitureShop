using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.ViewModel;

public class OrderView
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }

    public Guid UserId { get; set; }
    public virtual AppUser? User { get; set; }

    public EOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
}