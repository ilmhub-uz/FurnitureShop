using FurnitureShop.Api.Services;
using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationView>>> GetOrganizations([FromQuery] PaginationParams paginationParams) =>
        await _organizationService.GetOrganizationsAsync(paginationParams);

    [HttpGet("{organizationId:guid}")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrganizationView>> GetOrganizationById(Guid organizationId) =>
        await _organizationService.GetOrganizationByIdAsync(organizationId);
}