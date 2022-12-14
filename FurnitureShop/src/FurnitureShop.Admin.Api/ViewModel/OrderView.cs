using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Admin.Api.ViewModel;

public class OrderView
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public EOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProductView>? OrderProducts { get; set; }
}
