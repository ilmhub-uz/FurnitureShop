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
        [HttpPut("{contractId:guid}")]
        public async Task<IActionResult> UpdateContract([FromBody]UpdateContractDto updateContractDto , Guid contractId)
        {
            await _contractService.UpdateContract(contractId , updateContractDto);
            return Ok();
        }

        [HttpDelete("{contractId:guid}")]
        public async Task<IActionResult> DeleteContract([FromBody]UpdateContractDto updateContractDto , Guid contractId)
        {
            await _contractService.DeleteContract(contractId);
            return Ok();
        }

    }
}

