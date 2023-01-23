using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public EmployeeService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;   
        _userManager = userManager;
    }

    public async Task AddEmployee(ClaimsPrincipal appuser, AddEmployeeDto dto)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == dto.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var joiner = _userManager.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (joiner is null)
            throw new NotFoundException<AppUser>();

        var user = await _userManager.GetUserAsync(appuser);

        organization.Users!.Add(new OrganizationUser()
        {
            UserId = joiner.Id,
            User = joiner,
            OrganizationId = organization.Id,
            Organization = organization,
            Role = ERole.Manager
        });

        _unitOfWork.Save();
    }

    public async Task<List<GetEmployeesView>> GetManagers(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users!.Where(e => e.Role == ERole.Manager).ToList()).Adapt<List<GetEmployeesView>>()
            .Select(e =>
            {
                e.FirstName = _unitOfWork.AppUsers.GetById(e.UserId)?.FirstName;
                e.LastName = _unitOfWork.AppUsers.GetById(e.UserId)?.LastName;
                e.Email = _unitOfWork.AppUsers.GetById(e.UserId)?.Email;
                return e;
            }).ToList();
    }

    public async Task RemoveEmployee(RemoveEmployeeDto dto)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == dto.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var employee = organization.Users!.FirstOrDefault(u => u.UserId == dto.EmployeeId);
        if (employee is null)
            throw new NotFoundException<OrganizationUser>();

        organization.Users!.Remove(employee);
        _unitOfWork.Save();
    }

    public async Task<List<GetEmployeesView>> GetSellers(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users!.Where(e => e.Role == ERole.Seller).ToList()).Adapt<List<GetEmployeesView>>()
            .Select(e =>
            {
                e.FirstName = _unitOfWork.AppUsers.GetById(e.UserId)?.FirstName;
                e.LastName = _unitOfWork.AppUsers.GetById(e.UserId)?.LastName;
                e.Email = _unitOfWork.AppUsers.GetById(e.UserId)?.Email;
                return e;
            }).ToList();
    }

    public async Task<List<GetEmployeesView>> GetStaff(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users!.ToList()).Adapt<List<GetEmployeesView>>()
            .Select(e =>
            {
                e.FirstName = _unitOfWork.AppUsers.GetById(e.UserId)?.FirstName;
                e.LastName = _unitOfWork.AppUsers.GetById(e.UserId)?.LastName;
                e.Email = _unitOfWork.AppUsers.GetById(e.UserId)?.Email;
                return e;
            }).ToList();
    }
}
