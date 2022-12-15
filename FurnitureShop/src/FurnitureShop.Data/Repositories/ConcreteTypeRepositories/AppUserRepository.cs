using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
{
    public AppUserRepository(AppDbContext context) : base(context) { }
}