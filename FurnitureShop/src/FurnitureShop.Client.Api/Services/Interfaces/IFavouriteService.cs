using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IFavouriteService
{
    Task<List<FavouriteProductView>> GetUserFavourite(PaginationParams paginationParams, ClaimsPrincipal claims);
    Task AddToFavourite(ClaimsPrincipal claims, CreateFavouriteDto createFavouriteDto);
    Task DeleteFromFavouriteProductById(ClaimsPrincipal claims, Guid productId);
    Task DeleteFromFavouriteAllProducts(ClaimsPrincipal claims);
}
