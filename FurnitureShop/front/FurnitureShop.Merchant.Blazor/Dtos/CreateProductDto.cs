using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    [Required]
    public decimal Price { get; set; }
    public bool OnTrend { get; set; }
    public bool OnSale { get; set; }
    [Required]
    public uint Count { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [Required]
    public Guid OrganizationId { get; set; }
}