using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderView>> CreateOrder([FromBody] CreateOrderDto createOrderDto) =>
        await _orderService.CreateOrder(User, createOrderDto);

    [HttpGet]
    public async Task<ActionResult<List<OrderView>>> GetOrders([FromQuery] OrderFilterDto filter) =>
        await _orderService.GetOrders(filter, User);
    
    [HttpDelete("{orderId}/cancel")]
    public async Task<ActionResult> DeleteOrder(Guid orderId)
    {
        await _orderService.DeleteOrder( orderId);
       
        return Ok("order deleted");
    }
}
