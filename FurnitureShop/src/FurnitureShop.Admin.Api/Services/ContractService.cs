using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<ContractView>> GetContracts(ContractFilterDto filterDto)
        {

            var query =  _unitOfWork.Contracts.GetAll();
            //select from contracts;
            if (filterDto.ContractStatus is not null)
            {
                query = filterDto.ContractStatus switch
                {
                    EContractStatus.Created => query.Where(contract => contract.Status == EContractStatus.Created),
                    EContractStatus.Deleted => query.Where(contract => contract.Status == EContractStatus.Deleted),
                    EContractStatus.Confirmed => query.Where(contract => contract.Status == EContractStatus.Confirmed),
                    EContractStatus.Closed => query.Where(contract => contract.Status == EContractStatus.Closed),
                    _ => query,
                };
            }
            var allcontracts = await GetContract(filterDto, query);
            return allcontracts;
        }
        private async Task<List<ContractView>> GetContract(ContractFilterDto filterDto , IQueryable<Contract> query)
        {
            if (filterDto.SortStatus != null)
            {
                query = filterDto.SortStatus switch
                {
                    ESortStatus.Price => query.OrderByDescending(c => c.TotalPrice),
                    ESortStatus.CreatedDate => query.OrderByDescending(c => c.CreatedAt),
                    _ => query.OrderByDescending(c => c.Id)
                };
            }
            if (filterDto.OrderId != null)
                query = query.Where(contract => contract.OrderId == filterDto.OrderId);

            if (filterDto.UserId != null)
                query = query.Where(c => c.UserId == filterDto.UserId);
            if(filterDto.ProductId != null)
                query = query.Where(c => c.ProductId == filterDto.ProductId);

            //select from contracts where id = orderid and orgid= odrgod;
            var contracts = await query.ToPagedListAsync(filterDto);
            return  contracts.Adapt<List<ContractView>>();
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

