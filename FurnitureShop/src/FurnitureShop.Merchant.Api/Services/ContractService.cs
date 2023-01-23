﻿using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Contract = FurnitureShop.Data.Entities.Contract;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;
    public ContractService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ContractView>> GetContractsAsync(Guid oreganizationId)
    {
        var contracts = _unitOfWork.Contracts.GetAll();
        return contracts.Select(contract => contract.Adapt<ContractView>()).ToList();
    }

    public async Task<ContractView> GetContractByIdAsync(Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(c => c.Id == contractId);
        if (contract is null)
            throw new BadRequestException("Can't fount contract by Id") { ErrorCode = StatusCodes.Status404NotFound };

        return contract.Adapt<ContractView>();
    }

    public async Task<ContractView> AddContractAsync(Guid orderId)
    {
        var order = _unitOfWork.Orders.GetById(orderId);
        if (order is null)
            throw new BadRequestException("can't fount order by id");

        decimal totalPrice = 0;
        foreach (var orderProduct in order.OrderProducts!)
            totalPrice += orderProduct.Count * orderProduct.Product!.Price;

        var contract = new Contract()
        {
            UserId = order.UserId,
            Status = EContractStatus.Created,
            CreatedAt = DateTime.UtcNow,
            ProductCount = (uint)order.OrderProducts.Count(),
            TotalPrice = totalPrice,
            OrderId = order.Id
        };

        var createdContract = await _unitOfWork.Contracts.AddAsync(contract);
        if (createdContract is null)
            throw new BadRequestException("Contract can't create");

        return createdContract.Adapt<ContractView>();
    }

    public async Task DeleteContractAsync(Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAll().FirstOrDefaultAsync(c => c.Id == contractId);
        if (contract is null)
            throw new BadRequestException("Can't fount contract by Id") { ErrorCode = StatusCodes.Status404NotFound };

        await _unitOfWork.Contracts.Remove(contract);
    }
}
