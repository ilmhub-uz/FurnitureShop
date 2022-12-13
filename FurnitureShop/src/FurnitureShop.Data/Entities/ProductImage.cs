namespace FurnitureShop.Data.Entities;

public class ProductImage
{
    public Guid Id { get; set; }
    public string? ImagePath { get; set; }
    public int Order { get; set; }

    public Guid ProductId { get; set; }
    public virtual Product? Product { get; set; }

}