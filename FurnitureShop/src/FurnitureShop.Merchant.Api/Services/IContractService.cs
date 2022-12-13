using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;
public interface IContractService
{
    Task<List<ContractView>> GetContractsAsync();
    Task<ContractView> GetContractById(Guid contractId);
    Task AddContractAsync(CreateContractDto contractDto);
    Task DeleteContract(Guid contractId);
}
