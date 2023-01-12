using System.Security.Claims;
using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IEmployeeService
{
    Task AddEmployee(ClaimsPrincipal user, AddEmployeeDto dto);
    Task<List<GetEmployeesView>> GetManagers(Guid organizationId);
    Task RemoveEmployee(RemoveEmployeeDto dto);
    Task<List<GetEmployeesView>> GetSellers(Guid organizationId);
    Task<List<GetEmployeesView>> GetStaff(Guid organizationId);
}
