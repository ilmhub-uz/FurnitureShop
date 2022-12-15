﻿using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Services;

public interface IProductsService
{
    Task<List<ProductView>> GetProducts(PaginationParams paginationParams);
    Task<ProductView> GetProductByIdAsync(Guid productId);
    Task UpdateProduct(Guid productId, UpdateProductDto dtoModel);
    Task DeleteProductById(Guid productId);
}