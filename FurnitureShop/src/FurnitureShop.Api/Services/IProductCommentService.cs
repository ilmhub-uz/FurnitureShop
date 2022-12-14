using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Api.Services;

public interface IProductCommentService
{
    Task<List<ProductComment>> GetProductComments(Guid ProductId);
    Task<ProductComment> AddProductComments(Guid ProductId, ProductCommentView commentDto);
}
