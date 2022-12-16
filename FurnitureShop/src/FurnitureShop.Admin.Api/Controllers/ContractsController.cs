using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractsService _service;
        public ContractsController(IContractsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts([FromQuery]ContractsFilterDto filter)
        {
            var contracts = await _service.GetContracts(filter);
            return Ok(contracts);
        }

        [HttpGet("{contractId:Guid}")]
        public async Task<IActionResult> GetContract(Guid contractId)
        {
            var contract = await _service.GetContract(contractId);
            return Ok(contract);
        }
        [HttpGet("{contractId:Guid}")]
        public async Task<IActionResult> UpdateContract(Guid contractId, [FromBody] UpdateContractDto contractDto)
        {
            await _service.UpdateContract(contractId,contractDto);
            return Ok();
        }
        [HttpGet("{contractId:Guid}")]
        public async Task<IActionResult> DeleteContract(Guid contractId)
        {
            await _service.DeleteContract(contractId);
            return Ok();
        }
    }
}
