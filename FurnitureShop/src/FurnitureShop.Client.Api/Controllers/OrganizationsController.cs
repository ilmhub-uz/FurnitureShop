﻿using FurnitureShop.Client.Api.Services.Interfaces;
using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Filters;
using FurnitureShop.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

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
    public async Task<ActionResult<List<OrganizationView>>> GetOrganizations([FromQuery]PaginationParams filter) =>
        await _organizationService.GetOrganizationsAsync(filter);

    [HttpGet("{organizationId:guid}")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrganizationView>> GetOrganizationById(Guid organizationId) =>
        await _organizationService.GetOrganizationByIdAsync(organizationId);
}