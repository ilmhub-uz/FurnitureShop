using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Dtos;

public class EmployeeServiceDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public AddEmployeeRole ERole { get; set; }

    [Required] 
    public Guid OrganizationId { get; set; }
}
