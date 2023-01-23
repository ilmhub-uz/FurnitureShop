namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderDto
{
  public List<CreateOrderProductDto> Products { get; set; }
}