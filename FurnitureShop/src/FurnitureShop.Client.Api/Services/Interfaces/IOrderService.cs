using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IOrderService
{
    Task<OrderView> CreateOrder(ClaimsPrincipal claims,CreateOrderDto createOrderDto);
    Task<List<OrderView>> GetOrders(OrderFilterDto orderFilter , ClaimsPrincipal User);
    Task DeleteOrder(UpdateOrderDto updateOrderDto, Guid orderId);
}