namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderDto
{
    public Guid ProductId { get; set; }
    public uint Count { get; set; }
}