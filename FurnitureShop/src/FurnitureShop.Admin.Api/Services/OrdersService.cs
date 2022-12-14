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

    public async Task<List<OrderView>> GetOrderByOrganizationId(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(o => o.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var orders = _unitOfWork.Orders.GetAll().Where(o => o.OrganizationId == organizationId);
        if (orders is null)
            throw new NotFoundException<Order>();

        return orders.Adapt<List<OrderView>>();
    }
}
