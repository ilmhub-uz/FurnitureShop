using System.ComponentModel.DataAnnotations;



namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateProfileDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Email { get; set; }
}
