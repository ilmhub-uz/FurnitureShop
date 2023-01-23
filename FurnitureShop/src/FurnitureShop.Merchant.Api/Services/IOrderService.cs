using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IOrderService
{
    Task<List<OrderView>?> GetOrders(OrderFilterDto orderFilter, Guid organizationId);
    Task<OrderView> GetOrder(Guid orderId);
    Task ChangeOrderStatus(Guid orderId, ChangeOrderStatusDto changeOrderStatus);
}
