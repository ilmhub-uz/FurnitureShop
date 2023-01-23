using FurnitureShop.Dashboard.Blazor.Dtos.Enums;

namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class UpdateProductDto
{
    public EProductStatus Status { get; set; }
    public int CategoryId { get; set; }
    public Guid OrganizationId { get; set; }
}