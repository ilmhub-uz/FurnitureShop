using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Api.ViewModel
{
        public class FavouriteView
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
