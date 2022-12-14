using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class Order
{
    public Guid Id { get; set; }
    
    public Guid OrganizationId { get; set; }
    [ForeignKey(nameof(OrganizationId))]
    public virtual Organization? Organization { get; set; }
    
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }

    public EOrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    public Guid ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public virtual Contract? Contract{ get; set; }
}