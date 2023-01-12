using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IProductCommentService
{
    Task<List<ProductCommentView>> GetProductCommentsAsync();
    Task<ProductCommentView> GetProductCommentByIdAsync(Guid commentId);
    Task<ProductCommentView> AddProductCommentAsync(CreateProductCommentDto commentDto);
    Task<ProductCommentView> UpdateProductComment(Guid commentId, UpdateProductCommentDto updateDto);
    Task DeleteProductComment(Guid commentId);
}
