using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Services;
[Scoped]
public class OrderService : IOrderService
{
    private readonly UnitOfWork _unitOfWork;
    public OrderService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task ChangeOrderStatus(Guid orderId, ChangeOrderStatusDto changeOrderStatus)
    {
        var order = _unitOfWork.Orders.GetAll().Where(x => x.Id == orderId).FirstOrDefault();
        if (order == null)
            throw new NotFoundException<Order>();

        if (order.Status != EOrderStatus.Created)
            throw new BadRequestException("Status already changed");

        decimal totalPrice = 0;
        foreach (var orderProduct in order.OrderProducts!)
            totalPrice += orderProduct.Count * orderProduct.Product!.Price;

        if (changeOrderStatus.eOStatus is EOStatus.Accepted)
        {
            var contract = new Contract()
            {
                OrderId = orderId,
                CreatedAt = DateTime.UtcNow,
                UserId = order.UserId,
                Status = EContractStatus.Created,
                ProductCount = (uint)order.OrderProducts.Count(),
                TotalPrice = totalPrice,
                FinishDate = changeOrderStatus.finishDate
            };
            _unitOfWork.Contracts.AddAsync(contract);
            order.Status = EOrderStatus.Accepted;
        }
        else
        {
            order.Status = EOrderStatus.Canceled;
        }
        _unitOfWork.Orders.Update(order);
        return Task.CompletedTask;

    }
    public async Task<OrderView> GetOrder(Guid orderId)
    {
        var order = await _unitOfWork.Orders.GetAll().Where(x => x.Id == orderId).FirstOrDefaultAsync();
        if (order == null)
            throw new NotFoundException<Order>();

        var orderView = new OrderView()
        {
            Id = order.Id,
            OrganizationName = order.Organization!.Name,
            UserName = order.User!.UserName,
            UserMail = order.User.Email,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            LastUpdatedAt = order.LastUpdatedAt,
            OrderProducts = order.OrderProducts.Adapt<ICollection<OrderProductView>>()
        };
        return orderView;
    }


    public async Task<List<OrderView>?> GetOrders(OrderFilterDto orderFilter, Guid organizationId)
    {

        var orders = _unitOfWork.Orders.GetAll().Where(order => order.OrganizationId == organizationId);

        orders = _unitOfWork.Orders.GetAll().Where(order => order.Status == EOrderStatus.Created);

        if (orderFilter.CreatedAt is not null)
            orders = _unitOfWork.Orders.GetAll().Where(or => or.CreatedAt == orderFilter.CreatedAt);

        if (orderFilter.ProductId is not null)
            orders = _unitOfWork.Orders.GetAll().Where(or => or.OrderProducts.Any(p => p.ProductId == orderFilter.ProductId));

        var orderlist = await orders.ToPagedListAsync(orderFilter);

        var listOrderView = new List<OrderView>();
        foreach (var order in orderlist)
        {
            var orderView = new OrderView()
            {
                Id = order.Id,
                OrganizationName = order.Organization!.Name,
                UserName = order.User!.UserName,
                UserMail = order.User.Email,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                LastUpdatedAt = order.LastUpdatedAt,
                OrderProducts = order.OrderProducts.Adapt<ICollection<OrderProductView>>()
            };

            listOrderView.Add(orderView);
        }

        return listOrderView;
    }
}
