using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services;

public interface IContractsService
{
    Task<List<ContractView>> GetContracts(ContractsFilterDto filter);
    Task<ContractView> GetContract(Guid contractId);
    Task UpdateContract(Guid contractId, UpdateContractDto contractDto);
    Task DeleteContract(Guid contractId);
}
