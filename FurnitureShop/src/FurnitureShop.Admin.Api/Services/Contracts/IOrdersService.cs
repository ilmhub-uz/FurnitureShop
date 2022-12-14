﻿using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services.Contracts;

public interface IOrdersService
{
    Task<List<OrderView>> GetOrdersAsync();
    Task<OrderView> GetOrderByIdAsync(Guid orderId);
}
