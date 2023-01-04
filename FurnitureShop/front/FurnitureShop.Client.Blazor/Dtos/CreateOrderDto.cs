namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderDto
{
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Productni organizationidlari OrganizarionId bilan bir xil bolishi kerak
    /// </summary>
    public List<Guid>? CartProductIds { get; set; }
}