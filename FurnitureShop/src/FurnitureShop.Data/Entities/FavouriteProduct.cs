using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Entities
{
    public class FavouriteProduct
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public Guid FavouriteId { get; set; }
        public virtual Favourite? Favourite { get; set; }
    }
}
