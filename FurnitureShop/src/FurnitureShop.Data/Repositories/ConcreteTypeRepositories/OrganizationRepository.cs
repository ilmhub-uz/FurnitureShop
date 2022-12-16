using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using JFA.DependencyInjection;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

[Scoped]
public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository, IEntityExistsRepository
{
    public OrganizationRepository(AppDbContext context) : base(context) { }
}