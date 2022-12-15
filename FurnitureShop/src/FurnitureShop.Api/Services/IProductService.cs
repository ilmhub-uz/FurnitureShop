using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProducts(PaginationParams paginationParams);
    Task<ProductView> GetProductByIdAsync(Guid productId);
}