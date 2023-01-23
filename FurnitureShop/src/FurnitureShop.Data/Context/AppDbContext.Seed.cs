using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Data.Context;
public partial class AppDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<OrganizationUser>().HasKey(user => new { user.UserId, user.OrganizationId });
        builder.Entity<LikeProduct>().HasKey(likeproduct => new { likeproduct.UserId, likeproduct.ProductId });
        builder.Entity<Category>()
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedAdminRole(builder);
    }

    private void SeedAdminRole(ModelBuilder builder)
    {
        Dictionary<string, Guid> rolesId = new()
        {
            {"admin", Guid.Parse("3917758a-29cd-4f19-acf9-1115116ae21e")},
            {"moder", Guid.Parse("b4f0bbdb-c2de-4e9c-9e98-cd7fe87cb40a")},
            {"organization_owner", Guid.Parse("e4181450-14c3-4e25-a24f-e525a12dfabb")},
            {"organization_manager", Guid.Parse("8a464872-a276-4c16-847f-4616978471cd")},
            {"organization_seller", Guid.Parse("057cf7d0-4967-4b70-99c9-2495d3b66eb4")},
            {"client", Guid.Parse("229bb793-92ca-49ce-8d4f-138759036a58")}
        };

        var adminAccountId = Guid.Parse("530c4139-ef46-483c-bcd8-d57cb206429b");

        var adminPermissions = new List<EPermission>()
        {
            EPermission.CanCreateUser,
            EPermission.CanReadUser,
            EPermission.CanUpdateUser,
            EPermission.CanDeleteUser,
        };
        builder.Entity<AppUserRole>().HasData(new AppUserRole()
        {
            Id = rolesId["admin"],
            Name = "administrator",
            NormalizedName = "administrator".ToUpper(),
            ConcurrencyStamp = "eaa0097a-7f7e-4c49-865b-a6d880feb3ea",
            Permissions = adminPermissions
        });

        var appUser = new AppUser()
        {
            Id = adminAccountId,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = "administrator",
            NormalizedUserName = "ADMINISTRATOR"
        };

        PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "administrator1");

        builder.Entity<AppUser>().HasData(appUser);

        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = rolesId["admin"],
            UserId = adminAccountId
        });
    }
}