using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Services;

public interface IEmployeeService
{
    Task AddEmployee(AddEmployeeDto employeeDto);
    Task<List<GetEmployeesView>> GetManagers(Guid organizationId);
    Task RemoveManager(Guid organizationId, Guid managerId);
    Task AddSeller(ClaimsPrincipal User, Guid organizationId, string email);
    Task<List<GetEmployeesView>> GetSellers(Guid organizationId);
    Task RemoveSeller(Guid organizationId, Guid managerId);
}
