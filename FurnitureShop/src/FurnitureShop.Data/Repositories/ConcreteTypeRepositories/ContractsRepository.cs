using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

public class ContractsRepository : GenericRepository<Contract>,IContractsRepository
{
    public ContractsRepository(AppDbContext context) : base(context)
    {
    }
}
