using FurnitureShop.Api.Dtos;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Api.Services;

[Scoped]
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductView>> GetProductsAsync(ProductFilterDto productFilterDto)
    {
        var products = _unitOfWork.Products.GetAll();

        if (productFilterDto.WithInstallation is not null)
            products.Where(product => product.WithInstallation == productFilterDto.WithInstallation);
        
        if (productFilterDto.Brends is not null)
            products.Where(product => product.Brend != null && productFilterDto.Brends.Contains(product.Brend));
        
        if (productFilterDto.Materials is not null)
            products.Where(product =>
                product.Material != null && productFilterDto.Materials.Contains(product.Material));

        if (productFilterDto.FromPrice is not null)
            products.Where(product => product.Price >= productFilterDto.FromPrice);

        if (productFilterDto.ToPrice is not null)
            products.Where(product => product.Price <= productFilterDto.ToPrice);

        if (productFilterDto.IsAvailable is not null)
            products.Where(product => product.IsAvailable == productFilterDto.IsAvailable);

        if (productFilterDto.CategoriesId is not null)
            products.Where(product =>
                product.CategoryId != null && productFilterDto.CategoriesId.Contains(product.CategoryId.Value));

        if (productFilterDto.OrganizationsId is not null)
            products.Where(product => productFilterDto.OrganizationsId.Contains(product.OrganizationId));

        products = productFilterDto.SortingStatus switch
        {
            ESortStatus.Cheap => products.OrderBy(product=>product.Price),
            ESortStatus.Expensive => products.OrderByDescending(product => product.Price),
            ESortStatus.Rate => products.OrderByDescending(product => CalculateProductRate(product.Rates)),
            _ => products
        };


        return (await products.ToPagedListAsync((PaginationParams)productFilterDto)).Adapt<List<ProductView>>();
    }

    public async Task<ProductView> GetProductByIdAsync(Guid productId)
    {
        var existingProduct = await _unitOfWork.Products.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
        if (existingProduct is null)
            throw new NotFoundException<Product>();

        return existingProduct.Adapt<ProductView>();
    }

    private int CalculateProductRate(List<uint>? rates)
    {
        if (rates is null)
            return 0;

        return rates.Sum(rate=>(int)rate) / rates.Count;
    }
}