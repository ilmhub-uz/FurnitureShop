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

    public AdminHubs(AppDbContext context)
    {
        this.context = context;
    }

    [Authorize]
    public async Task GetAllStatistcs()
    {
        var categorysCount = context.Categories.Count();
        var organizationsCount = context.Organizations.Count();
        var productsCount = context.Products.Count();
        var usersCount = context.Users.Count();
        await Clients.All.SendAsync(nameof(GetAllStatistcs), categorysCount, organizationsCount, productsCount, usersCount);
    }
}