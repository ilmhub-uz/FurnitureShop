using Microsoft.AspNetCore.Identity;

namespace FurnitureShop.Data.Entities;

public class AppUserRole : IdentityRole<Guid>
{
    public List<EPermission>? Permissions { get; set; }
}