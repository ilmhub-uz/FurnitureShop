using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class CreateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}