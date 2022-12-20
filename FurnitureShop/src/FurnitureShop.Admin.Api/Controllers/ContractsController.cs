using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        
        private IContractService _contractService;
        private readonly IUnitOfWork _unitOfWork;
        
        public ContractsController(IUnitOfWork unitOfWork , IContractService contractService)
        {
            _unitOfWork = unitOfWork;
            _contractService = contractService;
        }
        [HttpPut("update/contract")]
        public async Task<IActionResult> UpdateContract(Guid contractId , UpdateContractDto updateContractDto)
        {
            await _contractService.UpdateContract(contractId , updateContractDto);
            return Ok();
        }

        [HttpDelete("delete/contract")]
        public async Task<IActionResult> DeleteContract(Guid contractId, UpdateContractDto updateContractDto)
        {
            await _contractService.DeleteContract(contractId);
            return Ok();
        }


        [HttpGet("get/contracts")]
        public async Task<IActionResult> GetContracts(ESortStatus sortStatus)
        {
           var contracts =  await _contractService.GetContracts(sortStatus);
            return Ok(contracts);
        }

    }
}

