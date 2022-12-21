using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Dtos.FilterDtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Services.Interfaces
{
    public interface IContractService
    {
        Task UpdateContract(Guid contractId, UpdateContractDto updateContractDto);
        Task DeleteContract(Guid contractId);
        Task<List<ContractView>> GetContracts(ContractFilterDto filterDto);
    }
}
