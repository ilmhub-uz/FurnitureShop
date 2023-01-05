namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderDto
{
    public Guid OrganizationId { get; set; }
    public List<CreateOrderProductDto>? CartProductIds { get; set; }
}