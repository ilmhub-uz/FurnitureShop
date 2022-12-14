using FurnitureShop.Client.Api.Dtos;
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
    [ProducesResponseType(typeof(OrderView), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrderView>> CreateOrder([FromBody] CreateOrderDto createOrderDto) =>
        await _orderService.CreateOrder(createOrderDto);

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrderView>>> GetOrders() => 
        await _orderService.GetOrders();

    [HttpPut("{orderId}/cancel")]
    [ProducesResponseType(typeof(OrderView), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrderView>> UpdateOrder(UpdateOrderDto updateOrderDto, Guid orderId) =>
        await _orderService.UpdateOrder(updateOrderDto, orderId);
}