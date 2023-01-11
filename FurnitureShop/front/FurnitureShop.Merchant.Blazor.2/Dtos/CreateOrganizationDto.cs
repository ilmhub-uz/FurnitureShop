using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Blazor.Dtos;

public class CreateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}