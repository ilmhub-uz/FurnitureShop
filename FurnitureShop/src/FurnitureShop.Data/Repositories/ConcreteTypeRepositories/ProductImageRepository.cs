using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository, IEntityExistsRepository
{
    public ProductImageRepository(AppDbContext context) : base(context) { }
}