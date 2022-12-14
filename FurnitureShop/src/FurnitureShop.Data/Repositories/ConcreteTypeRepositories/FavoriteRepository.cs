using System.Linq.Expressions;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class FavoriteRepository : GenericRepository<FavouriteProduct>, IFavoriteRepository
{
	public FavoriteRepository(AppDbContext context) : base(context) { }
    public IFavoriteRepository? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IFavoriteRepository? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<IFavoriteRepository> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IFavoriteRepository> Find(Expression<Func<IFavoriteRepository, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IFavoriteRepository> AddAsync(IFavoriteRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task AddRange(IEnumerable<IFavoriteRepository> entities)
    {
        throw new NotImplementedException();
    }

    public Task<IFavoriteRepository> Remove(IFavoriteRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRange(IEnumerable<IFavoriteRepository> entities)
    {
        throw new NotImplementedException();
    }

    public Task<IFavoriteRepository> Update(IFavoriteRepository entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRange(IEnumerable<IFavoriteRepository> entities)
    {
        throw new NotImplementedException();
    }
}
