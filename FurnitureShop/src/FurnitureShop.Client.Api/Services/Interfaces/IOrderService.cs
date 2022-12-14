using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IOrderService
{
    Task<OrderView> CreateOrder(CreateOrderDto createOrderDto);
    Task<List<OrderView>> GetOrders();
    Task<OrderView> UpdateOrder(UpdateOrderDto updateOrderDto, Guid orderId);
}