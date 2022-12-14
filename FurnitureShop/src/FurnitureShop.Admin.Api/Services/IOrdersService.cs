using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services;

public interface IOrdersService
{
    Task<List<OrderView>> GetOrdersAsync();
    Task<OrderView> GetOrderByIdAsync(Guid orderId);
    Task<List<OrderView>> GetOrderByOrganizationId(Guid organizationId);
}
