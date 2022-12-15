﻿using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services;

public interface IProductsService
{
    Task<List<ProductView>> GetProducts();
    Task<ProductView> GetProductByIdAsync(Guid productId);
    Task UpdateProduct(Guid productId, UpdateProductDto dtoModel);
    Task DeleteProductById(Guid productId);
}