using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Entities;
using System.Security.Claims;

namespace FurnitureShop.Api.Services;

public interface IProductCommentService
{
    Task<List<ProductCommentView>> GetProductComments(Guid productId);
    Task AddProductComments(AppUser user, Guid productId, CreateProductComment commentDto);
}
