using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;
    private readonly IValidator<CreateOrganizationDto> _createOrganizationValitor;
    private readonly IValidator<UpdateOrganizationDto> _updateOrganizationValidator;

    public OrganizationsController(IOrganizationService organizationService,
        IValidator<CreateOrganizationDto> createOrganizationValitor,
        IValidator<UpdateOrganizationDto> updateOrganizationValidator)
    {
        _organizationService = organizationService;
        _createOrganizationValitor = createOrganizationValitor;
        _updateOrganizationValidator = updateOrganizationValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationView>>> GetOrganizations() =>
        await _organizationService.GetOrganizationsAsync();

    [HttpGet("{organizationId:guid}")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<ActionResult<OrganizationView>> GetOrganizationById(Guid organizationId) =>
        await _organizationService.GetOrganizationByIdAsync(organizationId);

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
    {
        var validateResult = _createOrganizationValitor.Validate(createOrganizationDto);

        if (!validateResult.IsValid)
            return BadRequest();

        await _organizationService.AddOrganization(User, createOrganizationDto);
        return Ok();
    }

    [HttpPut("{organizationId:guid}")]
    [IdValidation]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromBody] UpdateOrganizationDto updateOrganizationDto)
    {
        var validateResult = _updateOrganizationValidator.Validate(updateOrganizationDto);

        if (!validateResult.IsValid)
            return BadRequest();

        await _organizationService.UpdateOrganization(organizationId, updateOrganizationDto);
        return Ok();
    }

    [HttpDelete("{organizationId:guid}")]
    [IdValidation]
    public async Task<IActionResult> DeleteOrganization(Guid organizationId)
    {
        await _organizationService.DeleteOrganization(organizationId);
        return Ok();
    }

    [HttpGet("{organizationName}")]
    public async Task<ActionResult<OrganizationView>> GetOrganizationByName(string organizationName) =>
        await _organizationService.GetOrganizationByNameAsync(organizationName);
}