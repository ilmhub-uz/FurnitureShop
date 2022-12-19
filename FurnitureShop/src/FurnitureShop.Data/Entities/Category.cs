using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Views { get; set; }
    public Guid? CategoryImageId { get; set; }

    [ForeignKey(nameof(CategoryImageId))]
    public virtual CategoryImage? CategoryImage { get; set; }
    public int? ParentId { get; set; }
    public virtual Category? Parent { get; set; }

    public virtual List<Category>? Children { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}