using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;
public class CreateContractDto
{
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Dictionary<string, string>? ProducProperties { get; set; }
    [Required]
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
}
