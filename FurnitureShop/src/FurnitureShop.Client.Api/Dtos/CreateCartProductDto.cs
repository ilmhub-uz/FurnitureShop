using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class CreateCartProductDto
{
    public Guid ProductId { get; set; }
    public uint Count { get; set; }
    public string? Properties { get; set; }
}