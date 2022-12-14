using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class FavoritesProduct
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    public Guid FavoritesId { get; set; }

    [ForeignKey(nameof(FavoritesId))]
    public virtual Favorites? Favorites { get; set; }
}
