using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Entities
{
    public class CategoryImage
    {
        public Guid Id { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
       
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
    }
}
