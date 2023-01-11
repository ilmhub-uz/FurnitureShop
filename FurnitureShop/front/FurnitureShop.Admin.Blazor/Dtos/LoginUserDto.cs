using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class LoginUserDto
{
    [Required]
    [StringLength(maximumLength: 30, ErrorMessage = "Username length can't be more than 3.", MinimumLength = 3)]
    public string? UserName { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string? Password { get; set; }
}
 