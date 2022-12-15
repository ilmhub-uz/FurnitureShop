using FurnitureShop.Merchant.Api.Dtos;
using FurnitureShop.Merchant.Api.ViewModel;

namespace FurnitureShop.Merchant.Api.Services;

public interface IEmployeeService
{
    Task AddEmployee(EmployeeServiceDto employeeDto);
    Task<List<GetEmployeesView>> GetManagers(Guid organizationId);
    Task RemoveEmployee(EmployeeServiceDto dto);
    Task<List<GetEmployeesView>> GetSellers(Guid organizationId);
    Task<List<GetEmployeesView>> GetStaff(Guid organizationId);
}
