using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Merchant.Api.Hubs;

//[Authorize]
public class Hubs : Hub
{
    private readonly AppDbContext _context;
    private readonly IOrganizationService _organizationService;
    public Hubs(AppDbContext context, IOrganizationService organizationService)
    {
        _context = context;
        _organizationService = organizationService;
    }

    public async Task GetOrganization(Guid organizationId)
    {
        var organization = await _organizationService.GetOrganizationByIdAsync(organizationId);

        await Clients.All.SendAsync("GetOrganization", organization);
    }
}
