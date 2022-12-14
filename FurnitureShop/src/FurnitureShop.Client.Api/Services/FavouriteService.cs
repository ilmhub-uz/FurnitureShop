using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Client.Api.Services
{
    public class FavouriteService : IFavouriteService
    {
        private readonly AppDbContext _context;
        
        public FavouriteService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddToFavourites(ClaimsPrincipal claims, Guid productId, CreateFavouriteDto dtoModel)
        {
            var favourite = dtoModel.Adapt<FavouriteProduct>();
            var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            favourite.UserId = userId;
            new FavouriteProduct()
            {
                ProductId = productId
            };

            await _context.FavouriteProducts.AddAsync(favourite);
            _context.SaveChanges();
        }

        public async Task<FavouriteView> GetFavouriteByIdAsync(Guid favouriteId)
        {
            var existingFavourite = await _context.FavouriteProducts.FirstOrDefaultAsync(f => f.Id == favouriteId);
            if(existingFavourite is null)
                throw new NotFoundException<FavouriteProduct>();

            _context.SaveChanges();
            return existingFavourite.Adapt<FavouriteView>();
        }

        public async Task RemoveFavourites(Guid favouriteId)
        {
            var favourite = await _context.FavouriteProducts.FirstAsync(f => f.Id == favouriteId);

            if (favourite is null)
                throw new Exception();

            _context.FavouriteProducts.Remove(favourite);
            _context.SaveChanges();
        }

    }
}
