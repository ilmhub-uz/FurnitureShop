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
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class OrderService : IOrderService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IValidator<CreateOrderDto> _createorderdtovalidator;
    public OrderService(UnitOfWork unitOfWork, IValidator<CreateOrderDto> createorderdtovalidator)
    {
        _unitOfWork = unitOfWork;
        _createorderdtovalidator = createorderdtovalidator;
    }

    public UnitOfWork Get_unitOfWork()
    {
        return _unitOfWork;
    }

    public async Task<OrderView> CreateOrder(ClaimsPrincipal claims, CreateOrderDto createOrderDto)
    {
        var result = _createorderdtovalidator.Validate(createOrderDto);
        if(!result.IsValid)
        {
            throw new BadRequestException(result.Errors.First().ErrorMessage);
        }
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (userId ==  null) throw new Exception("UserId is Not Found Ro'yxatdan o'ting"); 
        /*if (createOrderDto is null)
            throw new BadRequestException("createOrderDto is not null");*/

        //korsatilgan productlar bir hil organizationga tegishliligini tekshirish
        //qaysidir productdan organizationid sini olish
        //orderni saqlash
        var product1 = _unitOfWork.Products.GetById(createOrderDto.Products[0].ProductId);
        if (product1 is null) throw new Exception("Siz product tanlamagansiz");

        Guid orgId = product1.OrganizationId;
        foreach (var p in createOrderDto.Products)
        {
            var product = _unitOfWork.Products.GetById(p.ProductId);

            if (product is null) throw new Exception("Siz mavjud bo'lmagan productni tanladingiz");
            if (orgId != product.OrganizationId) throw new Exception("Siz tanlagan mahsulot bir xil organizationda bo'lishi kerak");
            // orgId = product.OrganizationId;
        }

        var newOrder = new Order()
        {
            UserId = userId,
            OrganizationId = orgId,
            Organization = product1.Organization,
            Status = EOrderStatus.Created,
            CreatedAt = DateTime.UtcNow,
        };

        var createorder = await _unitOfWork.Orders.AddAsync(newOrder);
        var orderProduct = new List<OrderProduct>();
        foreach (var p in createOrderDto.Products)
        {
            orderProduct.Add(new OrderProduct()
            {
                OrderId = createorder.Id,
                ProductId = p.ProductId,
                Count = p.Count,
            });
        }

        createorder.OrderProducts = orderProduct;
       var order = await _unitOfWork.Orders.Update(createorder);
        //var order = _unitOfWork.Orders.GetById(createorder.Id);

        if (order is null) return new OrderView();

        var organizationView = new OrganizationView()
        {
            Id = order.Organization.Id,
            Name = order.Organization.Name,
            Status = order.Organization.Status,
        };

        var orderView = new OrderView()
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            Organization = organizationView,
            User = order.User.Adapt<UserView>(),
            OrderProducts = order.Adapt<List<OrderProductView>>(),
        };

        return orderView;
    }

    public async Task<List<OrderView>> GetOrders(OrderFilterDto orderFilter, ClaimsPrincipal User)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        // null bo'lsa sign upga otish kerak
        var orders = _unitOfWork.Orders.GetAll().Where(order => order.UserId == userId);

        orders = orderFilter.OrderStatus switch
        {
            EOrderStatus.Created => _unitOfWork.Orders.GetAll().Where(or => or.Status == EOrderStatus.Created),
            EOrderStatus.Accepted => _unitOfWork.Orders.GetAll().Where(or => or.Status == EOrderStatus.Accepted),
            EOrderStatus.Canceled => _unitOfWork.Orders.GetAll().Where(or => or.Status == EOrderStatus.Canceled),
            _ => _unitOfWork.Orders.GetAll(),
        };

        if (orderFilter.CreatedAt is not null)
        {
            orders = _unitOfWork.Orders.GetAll().Where(or => or.CreatedAt == orderFilter.CreatedAt);
        }

        if (orderFilter.ProductId is not null)
        {
            orders = _unitOfWork.Orders.GetAll().Where(or => or.OrderProducts.Any(p => p.ProductId == orderFilter.ProductId));
        }
        if (orderFilter.OrganizationId is not null)
        {
            orders = _unitOfWork.Orders.GetAll().Where(or => or.OrganizationId == orderFilter.OrganizationId);
        }
        var orderlist = await orders.ToPagedListAsync(orderFilter);
        return orderlist.Adapt<List<OrderView>>();
    }

    public async Task<OrderView> DeleteOrder(UpdateOrderDto updateOrderDto, Guid orderId)
    {
        var existingOrder = await _unitOfWork.Orders.GetAll().FirstOrDefaultAsync(o => o.Id == orderId);
        if (existingOrder is null) throw new NotFoundException<Order>();

        if(existingOrder.Status == EOrderStatus.Accepted)
        {
            throw new Exception("Order uje accept qilingan, statusini o'zgartirolmaysiz");
        }
        else if(updateOrderDto.Status == EOrderStatus.Canceled)
        {
             await _unitOfWork.Orders.Remove(existingOrder);
        }

        return existingOrder.Adapt<OrderView>();
    }
}
