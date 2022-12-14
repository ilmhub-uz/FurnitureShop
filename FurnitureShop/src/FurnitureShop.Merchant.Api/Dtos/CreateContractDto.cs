using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;
public class CreateContractDto
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, string>? ProducProperties { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
}
