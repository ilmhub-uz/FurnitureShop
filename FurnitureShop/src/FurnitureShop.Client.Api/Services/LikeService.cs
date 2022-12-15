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
        public class LikeService : ILikeService
        {
            private readonly AppDbContext _context;

            public LikeService(AppDbContext appDbContext)
            {
                _context = appDbContext;
            }

            public async Task AddToLike(ClaimsPrincipal claims, Guid productId, CreateLikeDto dtoModel)
            {
                var like = dtoModel.Adapt<LikeProduct>();
                var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                like.UserId = userId;
                like.ProductId = productId;

                await _context.LikeProducts.AddAsync(like);
                _context.SaveChanges();
            }



            public async Task<LikeView> GetLikeByIdAsync(Guid likeId)
            {
                var existingLike = await _context.LikeProducts.FirstOrDefaultAsync(l => l.ProductId == likeId);
                if (existingLike is null)
                    throw new NotFoundException<LikeProduct>();

                return existingLike.Adapt<LikeView>();
            }

        public async Task RemoveLike(Guid likeId)
        {
            var like = await _context.LikeProducts.FirstAsync(l => l.ProductId == likeId);

            if (like is null)
                throw new Exception();

            _context.LikeProducts.Remove(like);
            _context.SaveChanges();
        }
    }
}
