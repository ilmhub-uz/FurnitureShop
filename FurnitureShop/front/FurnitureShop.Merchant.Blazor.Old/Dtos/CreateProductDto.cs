using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Blazor.Dtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool WithInstallation { get; set; }
    public string? Brend { get; set; }
    public string? Material { get; set; }
    //public Dictionary<string, string>? Properties { get; set; }
    [Required]
    public decimal? Price { get; set; }
    public bool IsAvailable { get; set; }
    [Required]
    public uint? Count { get; set; }

    [Required]
    public int? CategoryId { get; set; }
    [Required]
    public Guid? OrganizationId { get; set; }
}