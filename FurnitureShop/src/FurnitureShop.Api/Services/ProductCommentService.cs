using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FurnitureShop.Api.Services;

public class ProductCommentService : IProductCommentService
{
    public Task<ProductComment> AddProductComments(Guid ProductId, ProductCommentDto commentDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductComment>> GetProductComments(Guid ProductId)
    {
        throw new NotImplementedException();
    }
}
