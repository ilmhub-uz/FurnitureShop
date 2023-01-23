using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{
    private IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("/GetOrders/{organizationId:guid}")]
    public async Task<ActionResult<List<OrderView>?>> GetOrders([FromQuery] OrderFilterDto filter, Guid organizationId) =>
      await _orderService.GetOrders(filter, organizationId);

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<OrderView>> GetOrder(Guid orderId) =>
        await _orderService.GetOrder(orderId);

    [HttpPost("{orderId:guid}")]
    public async Task ChangeOrderStatus(Guid orderId, [FromBody] ChangeOrderStatusDto changeOrderStatus) =>
       await _orderService.ChangeOrderStatus(orderId, changeOrderStatus);
}



