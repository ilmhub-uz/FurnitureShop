namespace FurnitureShop.Admin.Api.Dtos;

public class OrderFilter : PagenationParams
{
    public Guid? OrganizationId { get; set; }
}