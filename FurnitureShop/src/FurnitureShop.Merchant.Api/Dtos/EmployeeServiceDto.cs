using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Dtos;

public class EmployeeServiceDto
{
    public Guid OrganizationId { get; set; }
    public string Email { get; set; }
    public AddEmployeeRole ERole { get; set; }
}
