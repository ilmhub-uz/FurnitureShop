using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }

    public virtual ICollection<CartProduct>? CartProducts { get; set; }
}