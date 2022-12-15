using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface ICartService
{
    Task<CartView> GetUserCart(PaginationParams paginationParams, Guid cartId, ClaimsPrincipal claims);
    Task AddToCart(ClaimsPrincipal claims, Guid productId, CreateCartDto createCartDto);
    Task DeleteCartProductById(Guid cartProductId, Guid productId);
    Task DeletCartAllProducts(Guid cartId);

}
