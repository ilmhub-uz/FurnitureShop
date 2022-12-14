using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Admin.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace FurnitureShop.Admin.Api.Controllers;

[Route("api/organizations")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationsService _service;

    public OrganizationsController(IOrganizationsService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrganizations()
    {
        var organization = await _service.GetOrganizationsAsync();
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