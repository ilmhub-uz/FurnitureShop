using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;
public interface IContractService
{
    Task<List<ContractView>> GetContractsAsync();
    Task<ContractView> GetContractByIdAsync(Guid orderId);
    Task<ContractView> AddContractAsync(Guid orderId,DateTime finishDate);
    Task DeleteContractAsync(Guid orderId);
}
