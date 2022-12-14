using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class Favorites
{
    [Key]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }

    public virtual ICollection<FavoritesProduct> FavoritesProducts { get; set; }
}
