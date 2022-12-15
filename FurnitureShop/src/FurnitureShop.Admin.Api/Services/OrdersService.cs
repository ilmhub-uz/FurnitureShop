using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
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
    public async Task<List<OrderView>> GetOrdersAsync(OrderFilter filter, PaginationParams paginationParams)
    {
        var orders = _unitOfWork.Orders.GetAll();

        if (filter.OrganizationId is not null)
        {
            orders = orders.Where(o => o.OrganizationId == filter.OrganizationId);
        }

        var orderList = await orders.ToPagedListAsync(paginationParams);
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
