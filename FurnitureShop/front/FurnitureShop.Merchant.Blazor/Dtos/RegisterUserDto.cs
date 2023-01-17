using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Blazor.Dtos;

public class RegisterUserDto
{
    [Required]
    [StringLength(8, ErrorMessage = "Username length can't be more than 8.")]
    public string? UserName { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Username length can't be more than 8.")]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    public string? Email { get; set; }
}