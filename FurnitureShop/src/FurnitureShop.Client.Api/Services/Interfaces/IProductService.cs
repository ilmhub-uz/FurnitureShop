using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductView>> GetProducts(ProductFilterDto filter);
    Task<ProductView> GetProductByIdAsync(Guid productId);
}