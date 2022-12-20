using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services
{
    [Scoped]
    public class ContractService : IContractService
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteContract(Guid contractId)
        {
            if (!await _unitOfWork.Contracts.GetAll().AnyAsync(contract => contract.Id == contractId))
                throw new NotFoundException<Contract>();
            var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(contract => contract.Id == contractId);
            contract!.Status = EContractStatus.Deleted;
            await _unitOfWork.Contracts.Update(contract);
        }

        public async Task<List<ContractView>> GetContracts(ESortStatus Status)
        {
            if (await _unitOfWork.Contracts.GetAll().ToListAsync() is null)
                return new List<ContractView>();

            var contracts = new List<ContractView>();
            if(Status == ESortStatus.Price)
            {
                contracts =  _unitOfWork.Contracts.GetAll()
                    .OrderBy(contract => contract.TotalPrice).ToListAsync()
                    .Adapt<List<ContractView>>();
            }
            else if(Status == ESortStatus.Name)
            {
                contracts = _unitOfWork.Contracts.GetAll()
                    .OrderBy(contract => contract.Product!.Name)
                    .ToListAsync().Adapt<List<ContractView>>();
            }
            else if(Status == ESortStatus.LastData)
            {
                contracts = _unitOfWork.Contracts.GetAll()
                    .OrderBy(contract => contract.CreatedAt)
                    .ToListAsync().Adapt<List<ContractView>>();
            }

            return contracts;
        }

        public async Task UpdateContract(Guid contractId, UpdateContractDto updateContractDto)
        {
            if (!await _unitOfWork.Contracts.GetAll().AnyAsync(contract => contract.Id == contractId))
                throw new NotFoundException<Contract>();

            var contract = await _unitOfWork.Contracts.GetAll().
                FirstOrDefaultAsync(con => con.Id == contractId);
            contract.Status = updateContractDto.Status;
            await _unitOfWork.Contracts.Update(contract);
        }

    
    }
}

