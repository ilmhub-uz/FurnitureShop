using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class CreateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public uint Count { get; set; }
    public int CategoryId { get; set; }
    public Guid OrganizationId { get; set; }
}