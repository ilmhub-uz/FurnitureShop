namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateUserDto
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IFormFile? Avatar { get; set; }
}