using FurnitureShop.Client.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok();
    }

    [HttpPut("{orderId}/cancel")]
    public async Task<IActionResult> UpdateOrder(Guid orderId)
    {
        return Ok();
    }
}