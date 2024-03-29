﻿using System.Security.Claims;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IProductService
{
    Task<List<ProductView>> GetProducts(ProductSortingFilter sortnigFilter, ClaimsPrincipal principal);
    Task<ProductView> GetProductByIdAsync(Guid productId);
    Task AddProduct(CreateProductDto dtoModel, ClaimsPrincipal principal);
    Task UpdateProduct(Guid productId, UpdateProductDto dtoModel, ClaimsPrincipal principal);
    Task DeleteProductById(Guid productId);
    Task<ProductView> AddOrUpdateProductImageAsync(Guid productId, CreateOrUpdateProductImageDto createProductImageDto);
}