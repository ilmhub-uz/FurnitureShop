using FurnitureShop.Api.Dtos;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FurnitureShop.Api.Services;

public class ProductCommentService : IProductCommentService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    public ProductCommentService(AppDbContext context, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task AddProductComments(ClaimsPrincipal principal, Guid ProductId, CreateProductComment commentDto)
    {
        var comment = commentDto.Adapt<ProductComment>();
        await _context.ProductComments.AddAsync(comment);
        await _context.SaveChangesAsync();
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
