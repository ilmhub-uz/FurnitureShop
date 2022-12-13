using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
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
    public async Task<List<ProductView>> GetProducts(ProductFilterDto filter)
    {
        var existingProducts = _unitOfWork.Products.GetAll();
        if (existingProducts is null)
            throw new NotFoundException<Product>();
        
        var arithmetic = ((int)filter.Page - 1) * (int)filter.Limit;
        
        var filteredProducts = await existingProducts
            .Skip(arithmetic)
            .Take((int)filter.Limit)
            .Select(e => e.Adapt<ProductView>())
            .ToListAsync();

        return filteredProducts;
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

        if (dtoModel.Count <= 0)
            throw new BadRequestException("Product count was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };

        if (dtoModel.Price <= 0)
            throw new BadRequestException("Product price was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };

        existingProduct = dtoModel.Adapt<Product>();

        await _unitOfWork.Products.Update(existingProduct);
    }

    public async Task DeleteProductById(Guid productId)
    {
        var existingProduct = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        await _unitOfWork.Products.Remove(existingProduct);
    }
}