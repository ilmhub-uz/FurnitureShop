using Contract = FurnitureShop.Data.Entities.Contract;
using System.Diagnostics.Contracts;

namespace FurnitureShop.Data.Repositories.ConcreteTypeRepositories
{
    public interface IContractsRepository:IGenericRepository<Contract>
    {
    }
}
