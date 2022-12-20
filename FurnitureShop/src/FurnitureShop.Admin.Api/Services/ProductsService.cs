using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Dtos.Enums;
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
    public async Task<List<ProductView>> GetProducts(ProductFilterDto filter)
    {
        var existingProducts = _unitOfWork.Products.GetAll();

        if (filter.OrganizationId is not null)
            existingProducts = existingProducts.Where(o => o.OrganizationId == filter.OrganizationId);
        if (filter.CategoryId is not null)
            existingProducts = existingProducts.Where(c => c.CategoryId == filter.CategoryId);
        if (filter.Price is not null)
            existingProducts = existingProducts.Where(p => p.Price == filter.Price);
        if (filter.Brend is not null)
            existingProducts = existingProducts.Where(p => p.Brend == filter.Brend);

        if(filter.Status != null)
        {
            existingProducts = filter.Status switch
            {
                EProductStatus.Created => existingProducts = existingProducts.Where(p=>p.Status == EProductStatus.Created),
                EProductStatus.Active => existingProducts = existingProducts.Where(p=>p.Status == EProductStatus.Active),
                EProductStatus.InActive => existingProducts = existingProducts.Where(p=>p.Status == EProductStatus.InActive),
                EProductStatus.Deleted => existingProducts = existingProducts.Where(p=>p.Status == EProductStatus.Deleted),
                _ => existingProducts
            };
        }

        if(filter.SortingName is not null)
        {
            existingProducts = filter.SortingName switch
            {
                EProductSorting.Name => existingProducts.OrderByDescending(p => p.Name),
                EProductSorting.Price => existingProducts.OrderByDescending(p => p.Price),
                EProductSorting.Views => existingProducts.OrderByDescending(p => p.Views)
            };
        }

        var productsList = await existingProducts.ToPagedListAsync(filter);
        return productsList.Adapt<List<ProductView>>();
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