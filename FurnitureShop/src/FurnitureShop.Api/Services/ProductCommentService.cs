using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FurnitureShop.Api.Services;

public class ProductCommentService : IProductCommentService
{
    private readonly AppDbContext _context;
    public ProductCommentService(AppDbContext context)
    {
        _context = context;
    }
    public Task<ProductComment> AddProductComments(Guid ProductId, ProductCommentView commentDto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductComment>> GetProductComments(Guid ProductId)
    {
        var comments = await _context.ProductComments.Where(c => c.ProductId == ProductId).ToListAsync();

        var commentDtoList = new List<ProductCommentView>();
        foreach (var comment in comments)
        {
            var commentDto = comment.Adapt<ProductCommentView>();
            commentDtoList.Add(commentDto);
        }

        return comments;
    }
}
