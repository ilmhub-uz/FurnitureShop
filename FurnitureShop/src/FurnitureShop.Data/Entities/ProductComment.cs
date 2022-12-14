using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Entities;

public class ProductComment
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ParentId { get; set; }
    public virtual ProductComment? Parent { get; set; }
    public virtual ICollection<ProductComment>? Children { get; set; }
}
