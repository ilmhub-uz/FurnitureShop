using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductView>> GetProducts();
    Task<ProductView> GetProductByIdAsync(Guid productId);
}