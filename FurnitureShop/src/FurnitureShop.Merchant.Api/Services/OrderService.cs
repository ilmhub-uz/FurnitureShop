using System.Security.Claims;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Dtos.Enums;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractService _contractService;

    public OrderService(IUnitOfWork unitOfWork, IContractService contractService)
    {
        _unitOfWork = unitOfWork;
        _contractService = contractService;
    }

    public async Task<List<OrderView>> GetOrdersAsync()
    {
        var orders = _unitOfWork.Orders.GetAll().ToList();
        return orders.Select(o => o.Adapt<OrderView>()).ToList();
    }

    public OrderView GetOrderById(Guid orderId)
    {
        var order = _unitOfWork.Orders.GetById(orderId);
        if(order is null)
            return null;

        var result = order.Adapt<OrderView>();
        return result;
    }

    public async Task<OrderView> UpdateOrderAsync(Guid orderId, UpdateOrderDto updateDto)
    {
        var order = _unitOfWork.Orders.GetById(orderId);
        if(order is null)
            return null;

        order.Status = updateDto.Status;
        order.LastUpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Orders.Update(order);

        if(order.Status == EOrderStatus.Accepted)
        {
            await _contractService.AddContractAsync(orderId);
        }

        return order.Adapt<OrderView>();
    }
}