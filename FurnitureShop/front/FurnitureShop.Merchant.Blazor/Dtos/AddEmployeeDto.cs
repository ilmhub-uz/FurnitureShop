using System.Security.Claims;

namespace FurnitureShop.Merchant.Blazor.Dtos;

public class AddEmployeeDto
{
    public Guid OrganizationId { get; set; }
    public string Email { get; set; }
    public AddEmployeeRole ERole { get; set; }
}
