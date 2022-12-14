using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    public decimal Price { get; set; }
    public virtual List<ProductImage>? Images { get; set; }
    public bool IsAvailable { get; set; }
    public uint Count { get; set; }
    public int Views { get; set; }
    public EProductStatus Status { get; set; }
    public List<uint>? Rates { get; set; }
    public int? CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category? Category { get; set; }

    public Guid OrganizationId { get; set; }
    [ForeignKey(nameof(OrganizationId))]
    public virtual Organization? Organization { get; set; }
}