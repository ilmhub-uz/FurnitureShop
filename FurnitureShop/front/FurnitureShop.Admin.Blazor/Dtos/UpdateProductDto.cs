using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateProductDto
{
    public EProductStatus Status { get; set; }
    public int CategoryId { get; set; }
    public Guid OrganizationId { get; set; }
}