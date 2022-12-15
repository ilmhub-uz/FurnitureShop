﻿using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories.ConcreteTypeRepositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppUserRole, Guid>
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationUser>? OrganizationUsers { get; set; }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage>? ProductImages { get; set; }
    public DbSet<ProductComment> ProductComments { get; set; }
    public DbSet<Contract>? Contracts { get; set; } 
    public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<OrganizationUser>().HasKey(user => new { user.UserId, user.OrganizationId });
    }

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