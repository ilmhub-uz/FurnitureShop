using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class FavouriteProduct
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
    public Guid FavouriteId { get; set; }
    [ForeignKey(nameof(FavouriteId))]
    public virtual Favourite? Favourite { get; set; }

}
