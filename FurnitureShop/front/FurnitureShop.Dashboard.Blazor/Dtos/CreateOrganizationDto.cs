using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class CreateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}