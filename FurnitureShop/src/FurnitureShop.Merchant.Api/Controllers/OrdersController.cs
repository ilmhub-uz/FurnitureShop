using FurnitureShop.Common.Filters;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Hubs;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private IHubContext<OrganizationHub> _hubContext;
    private IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrderView>>> GetOrders() 
        => await _orderService.GetOrdersAsync();

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<ActionResult<OrderView>> GetOrderById(Guid orderId)
    {
        var order = _orderService.GetOrderById(orderId);
        if(order is null)
            return BadRequest("Order can't found");

        return Ok(order);
    }

    [HttpPut("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateOrder(Guid orderId, [FromBody] UpdateOrderDto updateOrderDto)
    {
        var updatedOrder = await _orderService.UpdateOrderAsync(orderId, updateOrderDto);
        if(updatedOrder is null)
            return BadRequest("Can't updated order");

        return Ok(updatedOrder);
    }

}
