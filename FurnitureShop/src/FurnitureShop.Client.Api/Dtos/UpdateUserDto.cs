using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateUserDto
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
}