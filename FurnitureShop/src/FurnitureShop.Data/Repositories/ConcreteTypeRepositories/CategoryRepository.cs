using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository, IEntityExistsRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}