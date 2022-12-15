namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderDto
{
    public Guid OrganizationId { get; set; }

    public Guid UserId { get; set; }
    public List<CreateOrderProductDto>? CartProductIds { get; set; }
}