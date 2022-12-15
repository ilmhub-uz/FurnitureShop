using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class UpdateUserDto
{
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}