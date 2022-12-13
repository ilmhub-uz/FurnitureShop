using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.Dtos;

public class CreateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}