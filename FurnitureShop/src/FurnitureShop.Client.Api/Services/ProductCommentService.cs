using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services;

public class ProductCommentService : IProductCommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductCommentView>> GetProductCommentsAsync(Guid productId)
    {
        var product = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NotFoundException<Product>();

        var productCommentsViewList = new List<ProductCommentView>();

        if (product.ProductComments is null) return new List<ProductCommentView>();

        foreach (var productComment in product.ProductComments)
        {
            var productCommentView = ConvertToProductCommentView(productComment);
            productCommentsViewList.Add(productCommentView);
        }

        return productCommentsViewList;
    }

    private ProductCommentView ConvertToProductCommentView(ProductComment productComment)
    {
        var productCommentView = new ProductCommentView
        {
            ProductId = productComment.ProductId,
            Comment = productComment.Comment
        };

        if (productComment.Children is null)
            return productCommentView;

        foreach (var child in productComment.Children)
        {
            productCommentView.Children ??= new List<ProductCommentView>();
            productCommentView.Children.Add(ConvertToProductCommentView(child));
        }

        return productCommentView;
    }

    public async Task AddProductCommentAsync(ClaimsPrincipal claims, Guid productId, CreateProductCommentDto commentDto)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var existingProductComment = new ProductComment
        {
            Comment = commentDto.Comment,
            ParentId = commentDto.ParentId,
            UserId = userId,
            ProductId = productId,
            CreatedAt = DateTime.Now
        };

        await _unitOfWork.ProductComments.AddAsync(existingProductComment);
    }

    public async Task<ProductCommentView> UpdateProductComment(Guid productId, Guid commentId, UpdateProductCommentDto updateDto)
    {
        var product = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NotFoundException<Product>();

        var existingProductComment = product.ProductComments?.FirstOrDefault(pc => pc.Id == commentId);

        if (existingProductComment is null) throw new NotFoundException<ProductComment>();

        existingProductComment.Comment = updateDto.Comment;

        await _unitOfWork.ProductComments.Update(existingProductComment);
        return existingProductComment.Adapt<ProductCommentView>();
    }
    public async Task DeleteProductComment(Guid productId, Guid commentId)
    {
        var product = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) throw new NotFoundException<Product>();

        var existingProductComment = product.ProductComments?.FirstOrDefault(pc => pc.Id == commentId);

        if (existingProductComment is null) throw new NotFoundException<ProductComment>();

        await _unitOfWork.ProductComments.Remove(existingProductComment);
    }
}
