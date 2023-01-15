using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderProductDto
{
    public Guid ProductId { get; set; }
    public uint Count { get; set; }
}