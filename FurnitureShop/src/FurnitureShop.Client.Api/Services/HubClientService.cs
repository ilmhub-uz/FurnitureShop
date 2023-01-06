using JFA.DependencyInjection;

namespace FurnitureShop.Client.Api.Services;
[Scoped]
public class HubClientsService
{
    public List<string> Clients { get; set; } = new List<string>();
}