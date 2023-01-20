using System.Security.Claims;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Dtos.Enums;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddProduct(CreateProductDto dtoModel, ClaimsPrincipal principal)
    {
        var productEntity = dtoModel.Adapt<Product>();
        productEntity.AuthorId = Guid.Parse(principal.GetUserId());

        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == dtoModel.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var category = await _unitOfWork.Categories.GetAll().FirstOrDefaultAsync(c => c.Id == dtoModel.CategoryId);
        if (category is null)
            throw new NotFoundException<Category>();

        if (dtoModel.Count < 1)
            throw new BadRequestException("Product count was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest};

        if (dtoModel.Price <= 0)
            throw new BadRequestException("Product price was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };

        await _unitOfWork.Products.AddAsync(productEntity);
    }
    
    public async Task DeleteProductById(Guid productId)
    {
        var existingProduct = _unitOfWork.Products.GetById(productId);

        await _unitOfWork.Products.Remove(existingProduct!);
    }

    public async Task<ProductView> GetProductByIdAsync(Guid productId)
    {
        var existingProduct = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);

        return existingProduct!.Adapt<ProductView>();
    }

    public async Task<List<ProductView>> GetProducts(ProductSortingFilter sortingFilter, ClaimsPrincipal principal)
    {
        var existingProducts = _unitOfWork.Products.GetAll();

        if (sortingFilter.OnlyMyProducts is true)
        {
            var userId = Guid.Parse(principal.GetUserId());
            existingProducts = existingProducts.Where(p => p.OrganizationId == sortingFilter.OrganizationId && p.AuthorId == userId);
        }
        // else
        // {
        //     existingProducts = existingProducts.Where(p => p.OrganizationId == sortingFilter.OrganizationId);
        // }

        if (sortingFilter.Brend is not null)
            existingProducts = existingProducts.Where(p => p.Brend == sortingFilter.Brend);

        if(sortingFilter.Price is not null)
            existingProducts = existingProducts.Where(p => p.Price == sortingFilter.Price); 

        if(sortingFilter.CategoryId is not null)
            existingProducts = existingProducts.Where(p => p.CategoryId == sortingFilter.CategoryId);

        if(sortingFilter.SortingParams is not null)
        {
            existingProducts = sortingFilter.SortingParams switch
            {
                //ESortingParameters.ItemsSold => existingProducts.OrderByDescending(p => p.Count),
                ESortingParameters.Rate => existingProducts.OrderByDescending(p => p.Rates),
                ESortingParameters.Name => existingProducts.OrderBy(p => p.Name),
                ESortingParameters.Views => existingProducts.OrderBy(p => p.Views),
                _ => existingProducts
            };
        }

        var products = await existingProducts.ToPagedListAsync(sortingFilter);

        return products.Adapt<List<ProductView>>();
    }

    public async Task UpdateProduct(Guid productId, UpdateProductDto dtoModel, ClaimsPrincipal principal)
    {
        var userId = Guid.Parse(principal.GetUserId());

        var existingProduct = _unitOfWork.Products.GetById(productId);
        
        if(!existingProduct.Organization.Users.Any(u => u.UserId == userId))
            throw new BadRequestException("You have no access to update the product");

        var organization = _unitOfWork.Organizations.GetById(dtoModel.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var category = _unitOfWork.Categories.GetById(dtoModel.CategoryId);
        if (category is null)
            throw new NotFoundException<Category>();

        if (dtoModel.Count < 0)
            throw new BadRequestException("Product count was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };
        
        if (dtoModel.Price <= 0)
            throw new BadRequestException("Product price was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };

        existingProduct.Name = dtoModel.Name;
        existingProduct.Description = dtoModel.Description;
        existingProduct.WithInstallation = dtoModel.WithInstallation;
        existingProduct.Brend = dtoModel.Brend;
        existingProduct.Material = dtoModel.Material;
        existingProduct.Properties = dtoModel.Properties;
        existingProduct.Price = dtoModel.Price;
        existingProduct.IsAvailable = dtoModel.IsAvailable;
        existingProduct.Count = dtoModel.Count;
        existingProduct.CategoryId = dtoModel.CategoryId;
        existingProduct.OrganizationId = dtoModel.OrganizationId;

        await _unitOfWork.Products.Update(existingProduct);
    }

    public async Task<ProductView> AddOrUpdateProductImageAsync(Guid productId, CreateOrUpdateProductImageDto createProductImageDto)
    {
        var existingProduct = _unitOfWork.Products.GetById(productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        var productImages = createProductImageDto.ImageFile;
        if (productImages is null)
            throw new BadRequestException("Please enter product images.");

        existingProduct.Images = productImages;

        await _unitOfWork.Products.Update(existingProduct);

        return existingProduct.Adapt<ProductView>();
    }
}