using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface ICartService
{
    Task<CartView> GetUserCart(PaginationParams paginationParams, ClaimsPrincipal claims);
    Task AddToCart(ClaimsPrincipal claims, CreateCartProductDto createCartDto);
    Task DeleteCartProductById(ClaimsPrincipal claims, Guid productId);
    Task DeletCartAllProducts(ClaimsPrincipal claims);
}
