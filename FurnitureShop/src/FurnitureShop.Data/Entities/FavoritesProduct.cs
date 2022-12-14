using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class FavoritesProduct
{
    public Guid Id { get; set; }

    [Key]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }

    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
}
