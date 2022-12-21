using FurnitureShop.Admin.Api.Dtos.FilterDtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FurnitureShop.Admin.Api.Services.Interfaces;

public interface IOrdersService
{
    Task<List<OrderView>> GetOrdersAsync(OrderFilterDto filter);
    Task<OrderView> GetOrderByIdAsync(Guid orderId);
}
