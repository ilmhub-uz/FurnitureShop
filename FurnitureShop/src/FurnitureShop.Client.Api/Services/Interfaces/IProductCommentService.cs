using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IProductCommentService
{
    Task<List<ProductCommentView>> GetProductCommentsAsync(Guid productId);
    Task AddProductCommentAsync(ClaimsPrincipal claims, Guid productId, CreateProductCommentDto commentDto);
    Task<ProductCommentView> UpdateProductComment(Guid productId, Guid commentId, UpdateProductCommentDto updateDto);
    Task DeleteProductComment(Guid productId, Guid commentId);
}
