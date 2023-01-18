using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IContractService
{
    Task<List<ContractView>> GetContractsAsync();
    Task<ContractView> GetContractByIdAsync(Guid contractId);
}
