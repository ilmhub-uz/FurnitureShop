﻿using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services;


[Scoped]
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task AddToCart(ClaimsPrincipal claims, CreateCartProductDto createCartProductDto)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await _unitOfWork.Carts.GetAll().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart is null)
        {
            cart = new Cart()
            {
                UserId = userId,
            };

            await _unitOfWork.Carts.AddAsync(cart);
        }

        var product = new CartProduct()
        {
            ProductId = createCartProductDto.ProductId,
            Count = createCartProductDto.Count,
            Properties = createCartProductDto.Properties
        };

        cart.CartProducts ??= new List<CartProduct>();
        cart.CartProducts.Add(product);

        await _unitOfWork.Carts.Update(cart);
    }

    public async Task DeletCartAllProducts(ClaimsPrincipal claims)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await _unitOfWork.Carts.GetAll().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        if (cart.CartProducts is not null)
        {
            cart.CartProducts.Clear();
             _unitOfWork.Save();
        }
    }

    public async Task DeleteCartProductById(ClaimsPrincipal claims, Guid productId)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await _unitOfWork.Carts.GetAll().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        var product = cart.CartProducts?.FirstOrDefault(p => p.ProductId == productId);
        if (product is null)
        {
            throw new NotFoundException<Product>();
        }

        cart.CartProducts?.Remove(product);
        await _unitOfWork.Carts.Update(cart);
    }

    public async Task<CartView> GetUserCart(PaginationParams paginationParams, ClaimsPrincipal claims)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await _unitOfWork.Carts.GetAll().FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        var pagedList = cart.CartProducts?.AsQueryable().ToPagedList(paginationParams);
        cart.CartProducts ??= new List<CartProduct>();

        var productPaged = pagedList.Adapt<List<CartProductView>>();
        return cart.Adapt<CartView>();
    }
}
