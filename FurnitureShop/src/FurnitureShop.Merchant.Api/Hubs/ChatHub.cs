using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Merchant.Api.Hubs;
public class ChatHub : Hub
{
    private readonly AppDbContext _context;
    public ChatHub(AppDbContext context)
        => _context = context;
    
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
}
