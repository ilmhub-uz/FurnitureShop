using System.Security.Claims;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IOrderService
{
    Task<List<OrderView>> GetOrdersAsync();
    OrderView GetOrderById(Guid orderId);
    Task<OrderView> UpdateOrderAsync(Guid orderId, UpdateOrderDto updateDto);
}