using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Hubs;

public class AdminHubs : Hub
{
    private readonly AppDbContext context;
    private List<Category> Categoryes;  
    private List<Organization> Organizations;  
    private List<Product> Products;

    public AdminHubs(AppDbContext context)
    {
        this.context = context;
        Categoryes = this.context.Categories.ToList();
        Organizations = this.context.Organizations.ToList();
        Products = this.context.Products.ToList();
    }

    [Authorize]
    public async Task GetAllStatistcs()
    {
        await Clients.All.SendAsync(nameof(GetAllStatistcs), Categoryes.Count, Organizations.Count, Products.Count);
    }
}
