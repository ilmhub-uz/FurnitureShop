using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly IValidator<CreateContractDto> _createContractValidator;
    public ContractsController(IContractService contractService, 
        IValidator<CreateContractDto> createContractValidator)
    {
        _contractService = contractService;
        _createContractValidator = createContractValidator;
    }

    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto createContractDto)
    {
        var validateResult = _createContractValidator.Validate(createContractDto);
        
        if (!validateResult.IsValid)
            return BadRequest();

        await _contractService.AddContractAsync(createContractDto);

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ContractView>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await _contractService.GetContractsAsync();

        return Ok(contracts.Adapt<List<ContractView>>());
    }

    [HttpGet("{contractId:guid}")]
    [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<IActionResult>GetContractById(Guid contractId)
    {
        var category = await _contractService.GetContractByIdAsync(contractId);

        return Ok(category.Adapt<ContractView>());
    }

    [HttpDelete("{contractId:guid}")]
    [IdValidation]
    public async Task<IActionResult>DeleteProduct(Guid contractId)
    {
        await _contractService.DeleteContractAsync(contractId);

        return Ok();
    }
}
