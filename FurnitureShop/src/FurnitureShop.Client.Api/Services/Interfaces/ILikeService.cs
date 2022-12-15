using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces
{
    public interface ILikeService
    {
        Task<LikeView> GetLikeByIdAsync(Guid likeId);
        Task RemoveLike(Guid likeId);
        Task AddToLike(ClaimsPrincipal claims, Guid likeId, CreateLikeDto dtoModel);
    }
}
