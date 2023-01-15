using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class LoginUserDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}