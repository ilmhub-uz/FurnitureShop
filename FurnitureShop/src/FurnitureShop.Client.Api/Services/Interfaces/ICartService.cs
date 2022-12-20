using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface ICartService
{
    Task<List<CartProductView>> GetUserCart(PaginationParams paginationParams, Guid cartId);
    Task AddToCart(ClaimsPrincipal claims, Guid cartId, CreateCartProductDto createCartDto);
    Task DeleteCartProductById(Guid cartProductId, Guid productId);
    Task DeletCartAllProducts(Guid cartId);
}
