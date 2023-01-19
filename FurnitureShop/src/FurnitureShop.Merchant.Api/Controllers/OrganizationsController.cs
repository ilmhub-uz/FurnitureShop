using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Hubs;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;
    private IHubContext<OrganizationHub> _hubContext;
    private readonly IValidator<CreateOrganizationDto> _createOrganizationValitor;
    private readonly IValidator<UpdateOrganizationDto> _updateOrganizationValidator;

    public OrganizationsController(IOrganizationService organizationService,
        IHubContext<OrganizationHub> hubContext,
        IValidator<CreateOrganizationDto> createOrganizationValitor,
        IValidator<UpdateOrganizationDto> updateOrganizationValidator)
    {
        _organizationService = organizationService;
        _hubContext = hubContext;
        _createOrganizationValitor = createOrganizationValitor;
        _updateOrganizationValidator = updateOrganizationValidator;
    }


    [Authorize(EPermission.CanReadOrganization)]
    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationView>>> GetOrganizations([FromQuery]OrganizationSortingFilter filter) 
        => await _organizationService.GetOrganizationsAsync(filter, User);

    [HttpGet("{organizationId:guid}")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    [IdValidation]
    public async Task<ActionResult<OrganizationView>> GetOrganizationById(Guid organizationId) =>
        await _organizationService.GetOrganizationByIdAsync(organizationId);

    [Authorize(EPermission.CanCreateOrganization)]
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
    {
        var validateResult = _createOrganizationValitor.Validate(createOrganizationDto);

        if (!validateResult.IsValid)
            return BadRequest();

        await _organizationService.AddOrganization(User, createOrganizationDto);
        await _hubContext.Clients.All.SendAsync("ChangeOrganization");
        return Ok();
    }

    [Authorize(EPermission.CanUpdateOrganization)]
    [HttpPut("{organizationId:guid}")]
    //[IdValidation]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromBody] UpdateOrganizationDto updateOrganizationDto)
    {
        //var validateResult = _updateOrganizationValidator.Validate(updateOrganizationDto);

        //if (!validateResult.IsValid)
        //    return BadRequest();

        await _organizationService.UpdateOrganization(organizationId, updateOrganizationDto);
        await _hubContext.Clients.All.SendAsync("ChangeOrganization");
        return Ok();
    }

    [Authorize(EPermission.CanDeleteOrganization)]
    [HttpDelete("{organizationId:guid}")]
    [IdValidation]
    public async Task<IActionResult> DeleteOrganization(Guid organizationId)
    {
        await _organizationService.DeleteOrganization(organizationId);
        await _hubContext.Clients.All.SendAsync("ChangeOrganization");
        return Ok();    
    }

    [Authorize(EPermission.CanReadOrganization)]
    [HttpGet("{organizationName}")]
    public async Task<ActionResult<OrganizationView>> GetOrganizationByName(string organizationName) =>
        await _organizationService.GetOrganizationByNameAsync(organizationName);
}
