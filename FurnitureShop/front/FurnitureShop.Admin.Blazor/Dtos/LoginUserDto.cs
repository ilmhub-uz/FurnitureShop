using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class LoginUserDto
{
    [Required]
    [StringLength(8, ErrorMessage = "Username length can't be more than 8.")]
    public string? UserName { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string? Password { get; set; }
}
