using FurnitureShop.Client.Api.Services;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Client.Api.Hubs;

public class MessagesHub : Hub
{
    private readonly HubClientsService _hubClientsService;

    public MessagesHub(HubClientsService hubClientsService)
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