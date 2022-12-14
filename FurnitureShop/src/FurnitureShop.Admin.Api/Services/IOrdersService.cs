using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Admin.Api.Services;

public interface IOrdersService
{
    Task<List<OrderView>> GetOrdersAsync(OrderFilter filter);
    Task<OrderView> GetOrderByIdAsync(Guid orderId);
}
