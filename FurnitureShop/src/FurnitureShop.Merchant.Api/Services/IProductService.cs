using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProducts(ProductFilterDto filter);
    Task<ProductView> GetProductByIdAsync(Guid productId);
    Task AddProduct(CreateProductDto dtoModel);
    Task UpdateProduct(Guid productId, UpdateProductDto dtoModel);
    Task DeleteProductById(Guid productId);
    Task<ProductView> AddOrUpdateProductImageAsync(Guid productId, CreateOrUpdateProductImageDto createProductImageDto);
}