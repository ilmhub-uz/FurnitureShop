using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateOrganizationDto
{
    public string? Name { get; set; }
    public EOrganizationStatus Status { get; set; }
}







