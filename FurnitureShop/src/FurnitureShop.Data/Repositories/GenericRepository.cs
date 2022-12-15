using System.Linq.Expressions;
using FurnitureShop.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    protected DbSet<TEntity> DbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        DbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await DbSet.AddAsync(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);

        await _context.SaveChangesAsync();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        => DbSet.Where(expression);

    public IQueryable<TEntity> GetAll()
        => DbSet;

    public TEntity? GetById(int id)
        => DbSet.Find(id);

    public TEntity? GetById(Guid id)
        => DbSet.Find(id);

    public async Task<TEntity> Remove(TEntity entity)
    {
        var entry = DbSet.Remove(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        var entry = DbSet.Update(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateRange(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);

        await _context.SaveChangesAsync();
    }
    public async Task<bool> IsExists(object? id)
    {
        return await DbSet.FindAsync(id) != null;
    }
}