using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Controllers;
[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IOrdersService ordersService, IUnitOfWork unitOfWork)
    {
        _ordersService = ordersService;
        _unitOfWork = unitOfWork;
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

    [HttpGet("total-orders")]
    public async Task<IActionResult> GetTotalOrders([FromQuery] OrderFilterDto filter)
    {
        var orders = _unitOfWork.Orders.GetAll();
        var ordersCount = new OrdersCount();

        if (filter.OrganizationId != null && filter.Status != null)
        {
            orders = orders.Where(o => o.Status == filter.Status && o.OrganizationId == filter.OrganizationId);
            ordersCount.Status = filter.Status.ToString()!;
            ordersCount.OrganizationId = filter.OrganizationId;
        }
        if(filter.OrganizationId is not null)
        {
            orders = orders.Where(o => o.OrganizationId == filter.OrganizationId);
            ordersCount.OrganizationId = filter.OrganizationId;
        }
        if (filter.Status is not null)
        {
            orders = orders.Where(o => o.Status == filter.Status);
            ordersCount.Status = filter.Status.ToString()!;
        }

        ordersCount.TotalCount = await orders.CountAsync();
        return Ok(ordersCount);
    }
}
