using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
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

    [Authorize(EPermission.CanReadContract)]
    [HttpGet]
    [ProducesResponseType(typeof(List<ContractView>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await _contractService.GetContractsAsync();

        return Ok(contracts);
    }

    [Authorize(EPermission.CanReadContract)]
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<IActionResult>GetContractById(Guid orderId)
    {
        var contract = await _contractService.GetContractByIdAsync(orderId);

        return Ok(contract);
    }

    [Authorize(EPermission.CanDeleteContract)]
    [HttpDelete("{orderId:guid}")]
    [IdValidation]
    public async Task<IActionResult>DeleteContract(Guid orderId)
    {
        await _contractService.DeleteContractAsync(orderId);

        return Ok();
    }
}
