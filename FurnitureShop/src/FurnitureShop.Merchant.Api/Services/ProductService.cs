using System.Security.Claims;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Extensions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.Dtos;
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

    public async Task AddProduct(CreateProductDto dtoModel)
    {
        var productEntity = dtoModel.Adapt<Product>();
        
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

    public async Task<List<ProductView>> GetProducts()
    {
        return (await _unitOfWork.Products.GetAll().ToListAsync()).Adapt<List<ProductView>>();
    }

    public async Task UpdateProduct(Guid productId, UpdateProductDto dtoModel, ClaimsPrincipal principal)
    {
        var userId = Guid.Parse(principal.GetUserId());

        var existingProduct = _unitOfWork.Products.GetById(productId);

        if (!existingProduct!.Organization!.Users!.Any(u => u.UserId == userId))
            throw new BadRequestException("You have no access to update the product");

        if (dtoModel.OrganizationId is not null)
        {
            var organization = _unitOfWork.Organizations.GetById(dtoModel.OrganizationId ?? Guid.NewGuid());
            if (organization is null)
                throw new NotFoundException<Organization>();
        }

        if (dtoModel.OrganizationId is not null)
        {
            var category = _unitOfWork.Categories.GetById(dtoModel.CategoryId ?? 0);
            if (category is null)
                throw new NotFoundException<Category>();
        }

        if (dtoModel.Count is not null)
            if (dtoModel.Count < 0)
                throw new BadRequestException("Product count was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };
        if (dtoModel.Price is not null)
            if (dtoModel.Price <= 0)
                throw new BadRequestException("Product price was entered incorrectly") { ErrorCode = StatusCodes.Status400BadRequest };

        existingProduct.Name = dtoModel.Name ?? existingProduct.Name;
        existingProduct.Description = dtoModel.Description ?? existingProduct.Description;
        existingProduct.WithInstallation = dtoModel.WithInstallation ?? existingProduct.WithInstallation;
        existingProduct.Material = dtoModel.Material ?? existingProduct.Material;
        existingProduct.Brend = dtoModel.Brend ?? dtoModel.Brend;
        existingProduct.Properties = dtoModel.Properties ?? existingProduct.Properties;
        existingProduct.Price = dtoModel.Price ?? existingProduct.Price;
        existingProduct.IsAvailable = dtoModel.IsAvailable ?? existingProduct.IsAvailable;
        existingProduct.Count = dtoModel.Count ?? existingProduct.Count;
        existingProduct.CategoryId = dtoModel.CategoryId ?? existingProduct.CategoryId;
        existingProduct.OrganizationId = dtoModel.OrganizationId ?? existingProduct.OrganizationId;


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