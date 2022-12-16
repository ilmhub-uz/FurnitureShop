using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services;

public class ContractsService : IContractsService
{
    private readonly UnitOfWork _unitOfWork;

    public ContractsService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteContract(Guid contractId)
    {
        var contract = _unitOfWork.Contracts.GetById(contractId);
        if (contract is null)
            throw new NotFoundException<Contract>();

        contract.Status = EContractStatus.Closed;
       await _unitOfWork.Contracts.Update(contract);
    }

    public async Task<ContractView> GetContract(Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(c=>c.Id.Equals(contractId));
        if (contract is null)
            throw new NotFoundException<Contract>();

        return contract.Adapt<ContractView>();
    }

    public async Task<List<ContractView>> GetContracts(ContractsFilterDto filter)
    {
        var contracts = _unitOfWork.Contracts.GetAll();
        if(filter.UserId is not null)
        {
            contracts = contracts.Where(x => x.UserId == filter.UserId);
        }
        if(filter.ProductId is not null)
        {
            contracts = contracts.Where(x => x.ProductId == filter.ProductId);
        }
        if(filter.OrderId is not null)
        {
            contracts = contracts.Where(x => x.OrderId == filter.OrderId);
        }

        var contractsList =  await contracts.ToPagedListAsync(filter);
        return contractsList.Adapt<List<ContractView>>();
    }

    public async Task UpdateContract(Guid contractId, UpdateContractDto contractDto)
    {
        var contract = _unitOfWork.Contracts.GetById(contractId);
        if (contract is null)
            throw new NotFoundException<Contract>();

        contract.Status = contractDto.Status;
        await _unitOfWork.Contracts.Update(contract);
    }
}
