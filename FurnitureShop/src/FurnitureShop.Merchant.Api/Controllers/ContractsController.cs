﻿using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateContract([FromBody] Guid orderId)
    {
        var contract = await _contractService.AddContractAsync(orderId);
        return Ok(contract);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ContractView>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContracts(Guid organizationId)
    {
        var contracts = await _contractService.GetContractsAsync(organizationId);

        return Ok(contracts);
    }

    [HttpGet("{contractId:guid}")]
    [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<IActionResult>GetContractById(Guid contractId)
    {
        var category = await _contractService.GetContractByIdAsync(contractId);

        return Ok(category);
    }

    [HttpDelete("{contractId:guid}")]
    [IdValidation]
    public async Task<IActionResult>DeleteProduct(Guid contractId)
    {
        await _contractService.DeleteContractAsync(contractId);

        return Ok();
    }
}
