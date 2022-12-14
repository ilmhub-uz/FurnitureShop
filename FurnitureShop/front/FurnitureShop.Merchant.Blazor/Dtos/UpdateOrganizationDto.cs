using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}