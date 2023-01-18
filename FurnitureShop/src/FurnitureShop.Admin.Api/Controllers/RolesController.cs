using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.Services;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolesService _rolesService;
    private readonly RoleManager<AppUserRole> _roleManager;
    private readonly IValidator<CreateRoleDto> _createRoleValidator;
    private readonly IValidator<UpdateRoleDto> _updateRoleValidator;

    public RolesController(IRolesService rolesService, RoleManager<AppUserRole> roleManager, IValidator<CreateRoleDto> createRoleValidator, IValidator<UpdateRoleDto> updateRoleValidator)
    {
        _rolesService = rolesService;
        _roleManager = roleManager;
        _createRoleValidator = createRoleValidator;
        _updateRoleValidator = updateRoleValidator;
    }

    [HttpGet]
    [Authorize(EPermission.CanReadRole)]
    public async Task<IActionResult> GetRoles([FromQuery]RolesFilterDto filter) => Ok(await _rolesService.GetRolesAsync(filter));

    [HttpPost]
    [Authorize(EPermission.CanCreateRole)]
    public async Task<IActionResult> AddRole([FromBody]CreateRoleDto roleDto)
    {
        var result = _createRoleValidator.Validate(roleDto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var mappedRole = roleDto.Adapt<AppUserRole>();

        var createResult = await _roleManager.CreateAsync(mappedRole);
        
        if(!createResult.Succeeded)
            return BadRequest();

        return Ok();
    }

    [HttpPut]
    [Authorize(EPermission.CanUpdateRole)]
    public async Task<IActionResult> UpdateRole([FromQuery]Guid roleId, [FromBody]UpdateRoleDto roleDto)
    {
        var result = _updateRoleValidator.Validate(roleDto);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var retrievedRole = await _rolesService.GetRoleByIdAsync(roleId);

        retrievedRole.Name = roleDto.Name;
        retrievedRole.Permissions = roleDto.Permissions;

        var updateResult = await _roleManager.UpdateAsync(retrievedRole);

        if (!updateResult.Succeeded)
            return BadRequest();

        return Ok(retrievedRole);
    }

    [HttpDelete]
    [Authorize(EPermission.CanDeleteRole)]
    public async Task<IActionResult> DeleteRole([FromQuery]Guid roleId)
    {
        var retrievedRole = await _rolesService.GetRoleByIdAsync(roleId);

        var deleteRole = await _roleManager.DeleteAsync(retrievedRole);

        if(!deleteRole.Succeeded)
            return BadRequest();

        return Ok();
    }
}