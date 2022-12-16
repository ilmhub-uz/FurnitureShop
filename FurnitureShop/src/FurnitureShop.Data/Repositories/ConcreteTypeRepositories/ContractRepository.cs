using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories;

public class ContractRepository : GenericRepository<Contract>, IContractRepository
{
    public ContractRepository(AppDbContext context) : base(context)
    {
    }
}
