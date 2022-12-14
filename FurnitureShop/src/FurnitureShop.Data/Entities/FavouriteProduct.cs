using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities
{

    public class FavouriteProduct
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
