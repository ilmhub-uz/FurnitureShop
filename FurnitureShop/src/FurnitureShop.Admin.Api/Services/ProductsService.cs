using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;

public class ProductsService : IProductsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<ProductView>> GetProducts(PaginationParams paginationParams)
    {
        var products = await _unitOfWork.Products.GetAll().ToPagedListAsync(paginationParams);

        if (products is null)
            throw new NotFoundException<Product>();

        var productList = products.Select(p => p.Adapt<ProductView>()).ToList();

        return productList;
    }

    public async Task<ProductView> GetProductByIdAsync(Guid productId)
    {
        var existingProduct = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        return existingProduct.Adapt<ProductView>();
    }

    public async Task UpdateProduct(Guid productId, UpdateProductDto dtoModel)
    {
        var existingProduct = _unitOfWork.Products.GetAll().FirstOrDefault(p => p.Id == productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        var organization = _unitOfWork.Organizations.GetById(dtoModel.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var category = _unitOfWork.Categories.GetById(dtoModel.CategoryId);
        if (category is null)
            throw new NotFoundException<Category>();

        existingProduct.Status = dtoModel.Status;
        
        await _unitOfWork.Products.Update(existingProduct);
    }

    public async Task DeleteProductById(Guid productId)
    {
        var existingProduct = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();
        
        existingProduct.Status = EProductStatus.Deleted;
        
        await _unitOfWork.Products.Remove(existingProduct);
    }
}