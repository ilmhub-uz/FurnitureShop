using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace FurnitureShop.Admin.Api.Controllers;

[Route("api/organizations")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationsService _service;
    private readonly IValidator<UpdateOrganizationDto> _updateorganizationvalidator;
    public OrganizationsController(IOrganizationsService service , IValidator<UpdateOrganizationDto> validator)
    {
        _updateorganizationvalidator = validator;
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrganizations([FromQuery]OrganizationFilterDto filter)
    {
        var organization = await _service.GetOrganizationsAsync(filter);
        return Ok(organization);
    }

    [HttpGet("{organizationId:Guid}", Name = "OrganizationById")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrganization(Guid organizationId)
    {
        var organization = await _service.GetOrganizationByIdAsync(organizationId);
        return Ok(organization);
    }

    [HttpPut("{organizationId:Guid}")]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId,
        UpdateOrganizationDto updateOrganizationDto)
    {
        var result = await _updateorganizationvalidator.ValidateAsync(updateOrganizationDto);
        if (!result.IsValid)
        return BadRequest(result.Errors);
        
        await _service.UpdateOrganization(organizationId, updateOrganizationDto);
        return Ok();
    }
    [HttpDelete("{organizationId:Guid}")]
    public async Task<IActionResult> DeleteOrganization(Guid organizationId)
    {
        await _service.DeleteOrganization(organizationId);
        return Ok();
    }
}