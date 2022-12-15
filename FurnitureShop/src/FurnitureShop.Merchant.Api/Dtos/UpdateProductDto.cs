using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    public decimal Price { get; set; }
    public bool OnTrend { get; set; }
    public bool OnSale { get; set; }
    public uint? Count { get; set; }
    public int CategoryId { get; set; }
    public Guid OrganizationId { get; set; }
}