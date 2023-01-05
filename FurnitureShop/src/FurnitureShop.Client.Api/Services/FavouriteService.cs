using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services;


[Scoped]
public class FavouriteService : IFavouriteService
{
    private readonly IUnitOfWork _unitOfWork;


    public FavouriteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task AddToFavourite(ClaimsPrincipal claims, CreateFavouriteDto createFavouriteDto)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favourite = await _unitOfWork.Favorites.GetAll().FirstOrDefaultAsync(f => f.UserId == userId);
        if (favourite is null)
        {
            favourite = new Favourite()
            {
                UserId = userId
            };

            await _unitOfWork.Favorites.AddAsync(favourite);
        }

        if (favourite.FavouriteProducts.Any(p => p.ProductId == createFavouriteDto.ProductId))
        {
            throw new BadRequestException("Product already exists");
        }

        var product = new FavouriteProduct()
        {
            ProductId = createFavouriteDto.ProductId
        };
        favourite.FavouriteProducts ??= new List<FavouriteProduct>();
        favourite.FavouriteProducts.Add(product);

        await _unitOfWork.Favorites.Update(favourite);
    }

    public async Task DeleteFromFavouriteAllProducts(ClaimsPrincipal claims)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favourite = await _unitOfWork.Favorites.GetAll().FirstOrDefaultAsync(f => f.UserId == userId);
        if (favourite is null)
        {
            throw new NotFoundException<Favourite>();
        }

        if (favourite.FavouriteProducts is not null)
        {
            favourite.FavouriteProducts.Clear();
            _unitOfWork.Save();
        }
    }

    public async Task DeleteFromFavouriteProductById(ClaimsPrincipal claims, Guid productId)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favourite = await _unitOfWork.Favorites.GetAll().FirstOrDefaultAsync(f => f.UserId == userId);
        if (favourite is null)
        {
            throw new NotFoundException<Favourite>();
        }

        var product = favourite.FavouriteProducts.FirstOrDefault(p => p.ProductId == productId);
        if (product is null)
        {
            throw new NotFoundException<Product>();
        }

        favourite.FavouriteProducts.Remove(product);
        await _unitOfWork.Favorites.Update(favourite);

    }

    public async Task<List<FavouriteProductView>> GetUserFavourite(PaginationParams paginationParams, ClaimsPrincipal claims)
    {
        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var favourite = await _unitOfWork.Favorites.GetAll().FirstOrDefaultAsync(c => c.UserId == userId);
        if (favourite is null)
        {
            favourite = new Favourite()
            {
                UserId = userId
            };

            await _unitOfWork.Favorites.AddAsync(favourite);
        }

        var pagedList = favourite.FavouriteProducts?.AsQueryable().ToPagedList(paginationParams);
        favourite.FavouriteProducts ??= new List<FavouriteProduct>();

        var productPaged = pagedList.Adapt<List<FavouriteProductView>>();
        return productPaged;
    }
}
