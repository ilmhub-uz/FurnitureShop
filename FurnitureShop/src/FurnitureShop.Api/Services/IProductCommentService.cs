using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Entities;
using System.Security.Claims;

namespace FurnitureShop.Api.Services;

public interface IProductCommentService
{
    Task<List<ProductComment>> GetProductComments(Guid ProductId);
    Task AddProductComments(ClaimsPrincipal principal, Guid ProductId, CreateProductComment commentDto);
}
