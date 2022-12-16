﻿using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;

[Scoped]
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [IdValidation]
    public async Task<ProductView> GetProductByIdAsync(Guid productId)
    {
        var existingProduct = _unitOfWork.Products.GetById(productId);

        return existingProduct!.Adapt<ProductView>();
    }

    public async Task<List<ProductView>> GetProducts()
    {
        return (await _unitOfWork.Products.GetAll().ToListAsync()).Adapt<List<ProductView>>();
    }
}