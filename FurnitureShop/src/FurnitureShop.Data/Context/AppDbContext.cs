using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories.ConcreteTypeRepositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Data.Context;

public partial class AppDbContext : IdentityDbContext<AppUser, AppUserRole, Guid>
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationUser>? OrganizationUsers { get; set; }
    public DbSet<Favourite>? Favourites { get; set; }
    public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryImage> CategoryImages { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<Contract>? Contracts { get; set; } 
    public DbSet<Order>? Orders { get; set; }
    public DbSet<LikeProduct> LikeProducts { get; set; }
    public DbSet<Cart>? Carts { get; set; }
    public DbSet<CartProduct>? CartProducts { get; set; }
    public DbSet<Message> Messages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public override int SaveChanges()
    {
        UpdateProductStatus();

        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateProductStatus();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateProductStatus()
    {
        foreach (var entry in ChangeTracker.Entries<Product>())
        {
            if(entry.State == EntityState.Modified)
            {
                if (entry.Entity.Count > 0)
                    entry.Entity.IsAvailable = true;
                else
                    entry.Entity.IsAvailable = false;
            }
        }
    }
}