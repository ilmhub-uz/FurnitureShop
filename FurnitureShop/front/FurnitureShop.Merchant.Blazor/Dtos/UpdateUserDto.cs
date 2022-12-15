using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateUserDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}