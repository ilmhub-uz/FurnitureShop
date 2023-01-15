using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public ProductService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<ProductView> GetProductByIdAsync(Guid productId)
    {
        var existingProduct = _unitOfWork.Products.GetById(productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        return existingProduct.Adapt<ProductView>();
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
        if (filter.Rate is not null)
            existingProducts = existingProducts.Where(p => p.Rates.Select(t => (int)t).ToList().Sum() / p.Rates.Count() >= filter.Rate);
        if (filter.Name is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
                existingProducts = existingProducts.Where(p => p.Name.ToLower().Contains(filter.Name.Trim().ToLower()));
        }

        if (filter.ProductStatus != null)
        {
            existingProducts = filter.ProductStatus switch
            {
                EProductStatus.Created => existingProducts = existingProducts.Where(p => p.Status == EProductStatus.Created),
                EProductStatus.Active => existingProducts = existingProducts.Where(p => p.Status == EProductStatus.Active),
                EProductStatus.InActive => existingProducts = existingProducts.Where(p => p.Status == EProductStatus.InActive),
                EProductStatus.Deleted => existingProducts = existingProducts.Where(p => p.Status == EProductStatus.Deleted),
                _ => existingProducts
            };
        }

        if (filter.ProductSorting is not null)
        {
            existingProducts = filter.ProductSorting switch
            {
                EProductSorting.Name => existingProducts.OrderByDescending(p => p.Name),
                EProductSorting.Price => existingProducts.OrderByDescending(p => p.Price),
                EProductSorting.Views => existingProducts.OrderByDescending(p => p.Views)
            };
        }

        var productsList = await existingProducts.ToPagedListAsync(filter);
        return productsList.Adapt<List<ProductView>>();
    }
}