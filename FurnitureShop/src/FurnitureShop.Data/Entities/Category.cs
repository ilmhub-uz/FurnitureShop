namespace FurnitureShop.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int? ParentId { get; set; }
    public virtual Category? Parent { get; set; }

    public virtual List<Category>? Children { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}