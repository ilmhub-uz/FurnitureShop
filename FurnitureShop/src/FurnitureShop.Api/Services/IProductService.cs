using FurnitureShop.Api.ViewModel;

namespace FurnitureShop.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProducts();
    Task<ProductView> GetProductByIdAsync(Guid productId);
}