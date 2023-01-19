using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ContractView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ContractView>>> GetContractsAsync() =>
        await _contractService.GetContractsAsync();


    [HttpGet("{contractId:guid}")]
    [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
    public async Task<ActionResult<ContractView>> GetContractByIdAsync(Guid contractId) =>
        await _contractService.GetContractByIdAsync(contractId);
}
