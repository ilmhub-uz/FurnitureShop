using FluentValidation;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ValidateModel]
public class EmployeeController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmployeeService _employeeService;
    private readonly IValidator<AddEmployeeDto> validator;
    private readonly IValidator<RemoveEmployeeDto> _removeEmployeeValidator;


    public EmployeeController(IEmployeeService employeeService,
        UserManager<AppUser> userManager,
        IValidator<AddEmployeeDto> validator,
        IValidator<RemoveEmployeeDto> removeEmployeeValidator)
    {
        _employeeService = employeeService;
        _userManager = userManager;
        this.validator = validator;
        _removeEmployeeValidator = removeEmployeeValidator;
    }

    [Authorize(EPermission.CanCreateEmployee)]
    [HttpPost]
    public IActionResult AddEmployee([FromBody] AddEmployeeDto dto)
    {
        // var validationResult = validator.Validate(dto);
        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        _employeeService.AddEmployee(User, dto);
        return Ok();
    }

    [Authorize(EPermission.CanReadEmployee)]
    [HttpGet("managers/{organizationId:guid}")]
    public async Task<List<GetEmployeesView>> GetManagers(Guid organizationId)
        => await _employeeService.GetManagers(organizationId);

    [Authorize(EPermission.CanReadEmployee)]
    [HttpGet("sellers/{organizationId:guid}")]
    public async Task<List<GetEmployeesView>> GetSellers(Guid organizationId)
        => await _employeeService.GetSellers(organizationId);

    [Authorize(EPermission.CanReadEmployee)]
    [HttpGet("allStaff/{organizationId:guid}")]
    public async Task<List<GetEmployeesView>> GetStaff(Guid organizationId)
        => await _employeeService.GetStaff(organizationId);

    [Authorize(EPermission.CanDeleteEmployee)]
    [HttpDelete("{organizationId:guid}")]
    public async Task RemoveEmployee([FromBody] RemoveEmployeeDto dto)
    {
        // var validationResult = _removeEmployeeValidator.Validate(dto);
        // if (!validationResult.IsValid)
        //     throw new ValidationException(validationResult.Errors);

        await _employeeService.RemoveEmployee(dto);
    }
}
