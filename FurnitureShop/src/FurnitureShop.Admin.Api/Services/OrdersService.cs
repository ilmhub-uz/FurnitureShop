using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
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
    public async Task<List<OrderView>> GetOrdersAsync(OrderFilterDto filter)
    {
        var orders = _unitOfWork.Orders.GetAll();

        if (filter.OrganizationId is not null)
            orders = orders.Where(o => o.OrganizationId == filter.OrganizationId);
        if (filter.UserId is not null)
            orders = orders.Where(o => o.UserId == filter.UserId);
        if (filter.CreatedAt is not null)
            orders = orders.Where(o => o.CreatedAt == filter.CreatedAt);
        if (filter.ProductId is not null)
            orders = orders.Where(o => o.OrderProducts!.Any(p => p.ProductId == filter.ProductId));
        if (filter.ContractId is not null)
            orders = orders.Where(o => o.ContractId == filter.ContractId);

        if (filter.OrderStatus is not null)
        {
            orders = filter.OrderStatus switch
            {
                EOrderStatus.Created => orders.Where(o => o.Status == EOrderStatus.Created),
                EOrderStatus.Accepted => orders.Where(o => o.Status == EOrderStatus.Accepted),
                EOrderStatus.Canceled => orders.Where(o => o.Status == EOrderStatus.Canceled),
                _ => orders
            };
        }

        var orderList = await orders.ToPagedListAsync(filter);
        return orderList.Adapt<List<OrderView>>();
    }


    public async Task<OrderView> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _unitOfWork.Orders.GetAll().FirstOrDefaultAsync(o => o.Id == orderId);
        if (order is null)
            throw new NotFoundException<Order>();

        return order.Adapt<OrderView>();
    }
}
