using FurnitureShop.Admin.Api.Services.Contracts;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;
[Scoped]
public class OrdersService : IOrdersService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<OrderView>> GetOrdersAsync()
    {
        var orders = await _unitOfWork.Orders.GetAll().ToListAsync();
        return orders.Adapt<List<OrderView>>();
    }

    public async Task<OrderView> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _unitOfWork.Orders.GetAll().FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null)
            throw new NotFoundException<Order>();

        return order.Adapt<OrderView>();
    }
}
