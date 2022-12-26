using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateUserDto
{
    [Required(ErrorMessage = "username is required")]
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public EUserStatus UserStatus { get; internal set; }
}