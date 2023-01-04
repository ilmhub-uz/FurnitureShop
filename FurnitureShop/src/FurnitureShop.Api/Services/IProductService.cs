using FurnitureShop.Api.Dtos;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProductsAsync(ProductFilterDto productFilterDto);
    Task<ProductView> GetProductByIdAsync(Guid productId);
}