using Microsoft.AspNetCore.Identity;

namespace FurnitureShop.Data.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public EUserStatus Status { get; set; }
    public string? AvatarUrl { get; set; }

    public virtual ICollection<OrganizationUser>? Organizations { get; set; }
    public virtual ICollection<Contract>? Contracts { get; set; }
}