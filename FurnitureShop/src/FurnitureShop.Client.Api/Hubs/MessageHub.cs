using FurnitureShop.Client.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Client.Api.Hubs;

public class MessageHub : Hub
{
    private readonly HubClientsService _hubClientsService;

    public MessageHub(HubClientsService hubClientsService)
    {
        _hubClientsService = hubClientsService;
    }

    public override Task OnConnectedAsync()
    {
        _hubClientsService.Clients.Add(Context.ConnectionId);
        return Task.CompletedTask;
    }

    public async Task SendMessage(string username, string message)
    {
        await Clients.All.SendAsync("GetMessage", username, message);
    }
}