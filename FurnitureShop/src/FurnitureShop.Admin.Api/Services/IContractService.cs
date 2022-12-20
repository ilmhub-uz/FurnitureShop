﻿using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Services
{
    public interface IContractService
    {
        Task CreateContract(CreateContractDto createContracts);
        Task UpdateContract(Guid contractId , UpdateContractDto updateContractDto);
        Task DeleteContract(Guid contractId);
    }
}