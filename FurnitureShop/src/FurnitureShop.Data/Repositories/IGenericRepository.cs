using System.Linq.Expressions;

namespace FurnitureShop.Data.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? GetById(int id);
    TEntity? GetById(Guid id);
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRange(IEnumerable<TEntity> entities);
    Task<TEntity> Remove(TEntity entity);
    Task RemoveRange(IEnumerable<TEntity> entities);
    Task<TEntity> Update(TEntity entity);
    Task UpdateRange(IEnumerable<TEntity> entities);
}