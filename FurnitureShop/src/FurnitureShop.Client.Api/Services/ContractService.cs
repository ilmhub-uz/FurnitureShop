using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Client.Api.Services;
[Scoped]
public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContractService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ContractView> GetContractByIdAsync(Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(c => c.Id == contractId);

        if (contract is null) throw new NotFoundException<Contract>();

        return contract.Adapt<ContractView>();
    }

    public async Task<List<ContractView>> GetContractsAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetAll().ToListAsync();

        if (contracts is null) return new();

        return contracts.Adapt<List<ContractView>>();
    }
}
