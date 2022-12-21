using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;
public interface IContractService
{
    Task<List<ContractView>> GetContractsAsync(Guid organizationId);
    Task<ContractView> GetContractByIdAsync(Guid contractId);
    Task<ContractView> AddContractAsync(Guid orderId);
    Task DeleteContractAsync(Guid contractId);
}
