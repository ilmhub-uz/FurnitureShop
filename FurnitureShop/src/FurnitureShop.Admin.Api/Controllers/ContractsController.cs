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

        [HttpPut]
        public async Task<IActionResult> UpdateContract([FromQuery]Guid contractId, [FromBody]UpdateContractDto updateContractDto )

        {
            await _contractService.UpdateContract(contractId , updateContractDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContract([FromQuery]Guid contractId)

        {
            await _contractService.DeleteContract(contractId);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ContractFilterDto>), statusCode:StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts([FromQuery]ContractFilterDto contractFilter)
        {
         return Ok(await _contractService.GetContracts(contractFilter));
        }
    }
}

