using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;

public class ProductCommentService : IProductCommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductCommentView>> GetProductCommentsAsync()
    {
        var existingProductComments = await _unitOfWork.ProductComments.GetAll().ToListAsync();
        
        var productCommentsViewList = new List<ProductCommentView>();
        foreach (var productComment in existingProductComments)
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

    public async Task<ProductCommentView> GetProductCommentByIdAsync(Guid commentId)
    {
        var productComment = await _unitOfWork.ProductComments.GetAll().FirstOrDefaultAsync(p =>p.Id == commentId);

        if (productComment is null)
            throw new NotFoundException<ProductComment>();

        return ConvertToProductCommentView(productComment);
    }

    public async Task AddProductCommentAsync(CreateProductCommentDto commentDto)
    {
        var existingProductComment = new ProductComment
        {
            Comment = commentDto.Comment,
            ParentId = commentDto.ParentId
        };

        await _unitOfWork.ProductComments.AddAsync(existingProductComment);
    }

    public async Task<ProductCommentView> UpdateProductComment(Guid commentId, UpdateProductCommentDto updateDto)
    {
        var existingProductComment = _unitOfWork.ProductComments.GetById(commentId);

        if (existingProductComment is null)
            throw new NotFoundException<ProductCommentView>();

        await _unitOfWork.ProductComments.Update(existingProductComment);
        return existingProductComment.Adapt<ProductCommentView>();
    }
    public async Task DeleteProductComment(Guid commentId)
    {
        var existingProductComment = _unitOfWork.ProductComments.GetById(commentId);

        if (existingProductComment is null)
            throw new NotFoundException<ProductCommentView>();

        await _unitOfWork.ProductComments.Remove(existingProductComment);
    }
}
