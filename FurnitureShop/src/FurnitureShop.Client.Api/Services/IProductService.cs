using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProducts();
    Task<ProductView> GetProductByIdAsync(Guid productId);
}