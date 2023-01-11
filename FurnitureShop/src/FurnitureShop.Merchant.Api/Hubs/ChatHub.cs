using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Merchant.Api.Hubs;

//[Authorize]
public class ChatHub : Hub
{
    private readonly AppDbContext _context;
    private readonly IOrganizationService _organizationService;
    public ChatHub(AppDbContext context, IOrganizationService organizationService)
    {
        _context = context;
        _organizationService = organizationService;
    }
    
    public async Task SendMessageToUser(string senderId, 
        string receiverId,string content)
    {
        var sender = _context.Users.FirstOrDefault(u => u.Id.ToString() == senderId);
        var receiver = _context.Users.FirstOrDefault(u => u.Id.ToString() == receiverId);

        if(sender != null && receiver != null)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            //ma'lum bir clientga habar jo'natadi
            await Clients.User(receiverId).SendAsync("ReceiveMessage", sender.FirstName, content);
        }
    }

    // public async Task GetAllOrganizations()
    // {
    //     var sortingFilter = new OrganizationSortingFilter();
    //     var myOrganizations = await _organizationService.GetOrganizationsAsync(sortingFilter, User);
    //     await Clients.All.SendAsync(nameof(GetAllOrganizations), myOrganizations);
    // }
}
