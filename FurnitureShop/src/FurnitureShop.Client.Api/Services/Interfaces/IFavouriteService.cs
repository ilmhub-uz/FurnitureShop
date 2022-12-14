using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<FavouriteView> GetFavouriteByIdAsync(Guid favouriteId);
        Task RemoveFavourites(Guid favouriteId);
        Task AddToFavourites(ClaimsPrincipal claims, Guid productId, CreateFavouriteDto createFavouriteDto);
    }
}
