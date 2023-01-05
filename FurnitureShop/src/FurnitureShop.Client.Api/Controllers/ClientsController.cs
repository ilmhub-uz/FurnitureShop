using FurnitureShop.Client.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly HubClientsService _hubClientsService;

    public ClientsController(HubClientsService hubClientsService)
    {
        _hubClientsService = hubClientsService;
    }

    [HttpGet]
    public IActionResult GetClients() => Ok(_hubClientsService.Clients);
}