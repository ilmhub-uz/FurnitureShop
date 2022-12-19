using FurnitureShop.Common.Exceptions;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Context;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Contract = FurnitureShop.Data.Entities.Contract;

namespace FurnitureShop.Merchant.Api.Services;
public class ContractService : IContractService
{
    private readonly AppDbContext _context;

    public ContractService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContractView>> GetContractsAsync()
        => (await _context.Contracts!.ToListAsync()).Adapt<List<ContractView>>();

    public async Task<ContractView> GetContractByIdAsync(Guid contractId)
        => (await _context.Contracts.FirstOrDefaultAsync(c => c.Id == contractId)).Adapt<ContractView>();

    public async Task<ContractView> GetContractById(Guid contractId)
    {
        var contract = await _context.Contracts!.FirstAsync(c => c.Id == contractId);

        return contract.Adapt<ContractView>();
    }

    public async Task AddContractAsync(CreateContractDto contractDto)
    {
        var contract = contractDto.Adapt<Contract>();

        await _context.AddAsync(contract);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContractAsync(Guid contractId)
    {
        var contract = await _context.Contracts!.FirstOrDefaultAsync(c => c.Id == contractId);

        _context.Contracts!.Remove(contract!);
        _context.SaveChanges();
    }
}
