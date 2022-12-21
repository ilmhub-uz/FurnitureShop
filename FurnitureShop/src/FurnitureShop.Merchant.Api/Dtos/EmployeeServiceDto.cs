using FurnitureShop.Data.Entities;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Dtos;

public class EmployeeServiceDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public EUserStatus Status { get; set; }
    public Guid OrganizationId { get; set; }
    public string Email { get; set; }
    public AddEmployeeRole ERole { get; set; }
}
