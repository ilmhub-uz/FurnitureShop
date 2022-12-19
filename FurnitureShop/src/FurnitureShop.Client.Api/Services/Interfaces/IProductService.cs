using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductView>> GetProducts(PaginationParams paginationParams);
    Task<ProductView> GetProductByIdAsync(Guid productId);
}