using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class CartProduct
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    public Guid CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public virtual Cart? Cart { get; set; }

    public uint Count { get; set; }
    public string? Properties { get; set; }
}