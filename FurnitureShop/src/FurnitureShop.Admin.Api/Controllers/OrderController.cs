using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrderController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders([FromQuery] OrderFilterDto filter)
    {
        var orders = await _ordersService.GetOrdersAsync(filter);
        return Ok(orders);
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var order = await _ordersService.GetOrderByIdAsync(orderId);
        return Ok(order);
    }
}
