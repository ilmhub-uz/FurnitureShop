using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Entities
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int? Count { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
