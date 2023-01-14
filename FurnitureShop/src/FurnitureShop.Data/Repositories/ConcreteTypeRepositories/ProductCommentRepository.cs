using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class ProductCommentRepository : GenericRepository<ProductComment>, IProductCommentRepository, IEntityExistsRepository
{
    public ProductCommentRepository(AppDbContext context) : base(context) { }
}
