using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Admin.Api.Services
{
    public class ContractService : IContractService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateContract(CreateContractDto createContractDto)
        {
            var contract = new Contract()
            {
                OrderId = (Guid)createContractDto.OrderId,
            };

            await _unitOfWork.Contracts.AddAsync(contract);
        }

        public async Task DeleteContract(Guid contractId)
        {
            if (!await _unitOfWork.Contracts.GetAll().AnyAsync(contract => contract.Id == contractId))
                throw new NotFoundException<Contract>();
            var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(contract => contract.Id == contractId);
            contract.Status = EContractStatus.Deleted;
           
        }

        public async Task UpdateContract(Guid contractId , UpdateContractDto updateContractDto)
        {
            if(!await _unitOfWork.Contracts.GetAll().AnyAsync(contract=>contract.Id == contractId))
            throw new NotFoundException<Contract>();

            var contract = await _unitOfWork.Contracts.GetAll().
                FirstOrDefaultAsync(con => con.Id == contractId);
            contract.OrderId = (Guid)updateContractDto.OrderId;
            contract.Status = EContractStatus.Update;
            await _unitOfWork.Contracts.Update(contract);
        }
    }
}
