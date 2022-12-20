using FurnitureShop.Api.Dtos;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Api.Services;

[Scoped]
public class ProductCommentService : IProductCommentService
{
    private readonly AppDbContext _context;

    public ProductCommentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddProductComments(AppUser user, Guid productId, CreateProductComment commentDto)
    {
        var comment = commentDto.Adapt<ProductComment>();

        comment.UserId = user.Id;
        comment.ProductId = productId;

        await _context.ProductComments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProductCommentView>> GetProductComments(Guid productId)
    {
        var mainComments = await _context.ProductComments
            .Where(tc => tc.ParentId == null && tc.ProductId == productId)
            .ToListAsync();

        var mainCommentsView = new List<ProductCommentView>();

        foreach (var comment in mainComments)
        {
            var commentDto = ToProductCommentDto(comment);
            mainCommentsView.Add(commentDto);
        }

        return mainCommentsView;
    }

    private ProductCommentView ToProductCommentDto(ProductComment comment)
    {
        var commentDto = new ProductCommentView()
        {
            Id = comment.Id,
            Comment = comment.Comment,
            User = comment.User?.Adapt<UserView>(),
            CreatedDate = comment.CreatedAt

        };

        if (comment.Children is null)
            return commentDto;

        foreach (var child in comment.Children)
            commentDto.Children = new List<ProductCommentView> { ToProductCommentDto(child) };

        return commentDto;
    }
}
