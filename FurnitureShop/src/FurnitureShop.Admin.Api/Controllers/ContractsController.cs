using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;
    
    public ContractsController(IContractService contractService)
    {
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
    [ProducesResponseType(typeof(List<ContractFilterDto>), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts([FromQuery] ContractFilterDto contractFilter) => Ok(await _contractService.GetContracts(contractFilter));
}

