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
using System.Security.Claims;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Helpers;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class OrderService : IOrderService
{
    private readonly UnitOfWork _unitOfWork;

    public OrderService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderView> CreateOrder(ClaimsPrincipal claims,CreateOrderDto createOrderDto)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        if (createOrderDto is null)
            throw new BadRequestException("createOrderDto is not null");

        var newOrder = new Order()
        {
            UserId = userId,
            OrganizationId = createOrderDto.OrganizationId,
            OrderProducts = createOrderDto.CartProductIds.Select(id => new OrderProduct()
            {
                ProductId = id.ProductId,
                Count = id.Count,
                Properties = id.Properties
            }).ToList()
        };
        await _unitOfWork.Orders.AddAsync(newOrder);

        return newOrder.Adapt<OrderView>();
    }

    public async Task<List<OrderView>> GetOrders(OrderFilterDto orderFilter, ClaimsPrincipal User)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var orders = _unitOfWork.Orders.GetAll().Where(order => order.UserId == userId);
       
        orders = orderFilter.OrderStatus switch
        {
            EOrderStatus.Created =>  _unitOfWork.Orders.GetAll().Where(or=>or.Status == EOrderStatus.Created),
            EOrderStatus.Accepted => _unitOfWork.Orders.GetAll().Where(or=>or.Status == EOrderStatus.Accepted),
            EOrderStatus.Canceled => _unitOfWork.Orders.GetAll().Where(or=>or.Status == EOrderStatus.Canceled),
            _ => _unitOfWork.Orders.GetAll(),
        };

        if(orderFilter.CreatedAt is not null)
        {
          orders = _unitOfWork.Orders.GetAll().Where(or=>or.CreatedAt == orderFilter.CreatedAt);
        }

        if(orderFilter.ProductId is not null)
        {
            orders =  _unitOfWork.Orders.GetAll().Where(or => or.OrderProducts.Any(p => p.ProductId == orderFilter.ProductId));
        }
        if(orderFilter.OrganizationId is not null)
        {
            orders =  _unitOfWork.Orders.GetAll().Where(or => or.OrganizationId == orderFilter.OrganizationId);
        }
        var orderlist = await orders.ToPagedListAsync(orderFilter);
        return orderlist.Adapt<List<OrderView>>();
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

