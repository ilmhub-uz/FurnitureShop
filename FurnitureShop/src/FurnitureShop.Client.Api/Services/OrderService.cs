using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly UnitOfWork _unitOfWork;

    public OrderService(UnitOfWork unitOfWork, AppDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<OrderView> CreateOrder(CreateOrderDto createOrderDto)
    {
        if (createOrderDto is null)
            throw new BadRequestException("createOrderDto is not null");

        var newOrder = createOrderDto.Adapt<Order>();
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        return newOrder.Adapt<OrderView>();
    }

    public async Task<List<OrderView>> GetOrders()
    {
        return (await _unitOfWork.Orders.GetAll().ToListAsync()).Adapt<List<OrderView>>();
    }

    public async Task<OrderView> UpdateOrder(UpdateOrderDto updateOrderDto, Guid orderId)
    {
        var existingOrder = await _unitOfWork.Orders.GetAll().FirstOrDefaultAsync(o => o.Id == orderId);
        if (existingOrder is null) throw new NotFoundException<Order>();

        existingOrder.Status = updateOrderDto.Status;

        await _unitOfWork.Orders.Update(existingOrder);

        return existingOrder.Adapt<OrderView>();
    }
}

