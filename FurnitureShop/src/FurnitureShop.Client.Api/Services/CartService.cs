using FurnitureShop.Client.Api.Dtos;
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



    public async Task AddToCart(ClaimsPrincipal claims, Guid productId, CreateCartDto createCartDto)
    {
        var cart = createCartDto.Adapt<Cart>();
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        cart.UserId = userId;
        cart.CartProducts = new List<CartProduct>()
        {
            new CartProduct()
            {
                 ProductId = productId,
                 CartId = cart.Id
            }
        };

        await _unitOfWork.Carts.AddAsync(cart);
    }

    public async Task DeletCartAllProducts(Guid cartId)
    {
        var cart = _unitOfWork.Carts.GetById(cartId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        if (cart.CartProducts is not null)
        {
            await _unitOfWork.Carts.Remove(cart);
        }
    }

    public async Task DeleteCartProductById(Guid cartId, Guid productId)
    {
        var cart =  _unitOfWork.Carts.GetById(cartId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        var product = cart.CartProducts?.FirstOrDefault(p => p.Id == productId);
        if (product is null)
        {
            throw new NotFoundException<Product>();
        }

        cart.CartProducts?.Remove(product);
        await _unitOfWork.Carts.Update(cart);
    }

    public async Task<List<CartProductView>> GetUserCart(PaginationParams paginationParams, Guid cartId)
    {
        var cart = await _unitOfWork.Carts.GetAll().FirstOrDefaultAsync(c => c.Id == cartId);
        if (cart is null)
        {
            throw new NotFoundException<Cart>();
        }

        var pagedList = cart.CartProducts?.AsQueryable().ToPagedList(paginationParams);
        if (pagedList is null)
        {
            throw new NotFoundException<CartProduct>();
        }

        var result = pagedList.Adapt<List<CartProductView>>();
        return result;
    }
}
