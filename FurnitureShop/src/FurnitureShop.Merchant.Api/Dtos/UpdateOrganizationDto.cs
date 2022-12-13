using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class UpdateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}