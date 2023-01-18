using FurnitureShop.Admin.Blazor.Dtos.Enums;



namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateProductDto
{
    public EProductStatus Status { get; set; }
    public int CategoryId { get; set; }
    public Guid OrganizationId { get; set; }
}