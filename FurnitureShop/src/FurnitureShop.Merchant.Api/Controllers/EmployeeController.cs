using System.Security.Claims;
using FurnitureShop.Common.Filters;
using FurnitureShop.Data.Entities;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.Services;
using FurnitureShop.Merchant.Api.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Merchant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, UserManager<AppUser> userManager)
        {
            _employeeService = employeeService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeServiceDto dto)
        {
            await _employeeService.AddEmployee(User, dto);
            return Ok();
        }

        [HttpGet("managers")]
        public async Task<List<GetEmployeesView>> GetManagers(Guid organizationId) 
            => await _employeeService.GetManagers(organizationId);

        [HttpGet("sellers")]
        public async Task<List<GetEmployeesView>> GetSellers(Guid organizationId)
            => await _employeeService.GetSellers(organizationId);

        [HttpGet("allStaff")]
        public async Task<List<GetEmployeesView>> GetStaff(Guid organizationId)
            => await _employeeService.GetStaff(organizationId);

        [HttpDelete]
        public async Task RemoveEmployee(Guid organizationId, Guid employeeId)
            => await _employeeService.RemoveEmployee(organizationId, employeeId);
    }
}
