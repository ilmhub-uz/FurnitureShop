using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FurnitureShop.Common.Email_Sender.Services;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Services;

[Scoped]
public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<AppUser> _userManager;

    public EmployeeService(IUnitOfWork unitOfWork, IEmailSender emailSender, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _userManager = userManager;
    }

    public async Task AddEmployee(ClaimsPrincipal appuser, EmployeeServiceDto dto)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == dto.OrganizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var joiner = _userManager.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (joiner is null)
            throw new NotFoundException<AppUser>();

        var user = await _userManager.GetUserAsync(appuser);

        organization.Users.Add(new OrganizationUser()
        {
            UserId = joiner.Id,
            User = joiner,
            OrganizationId = organization.Id,
            Organization = organization,
            Role = ERole.Manager
        });

        var message = new EmailService(new string[] { $"{dto.Email}" }, "furnitureshop.uz organizations", $"{user.FirstName} has added you to {organization.Name} as manager. \n Congratulations 🎉");
        _emailSender.SendEmail(message);
    }

    public async Task<List<GetEmployeesView>> GetManagers(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users.Where(e => e.Role == ERole.Manager).ToList()).Adapt<List<GetEmployeesView>>();
    }

    public async Task RemoveEmployee(Guid organizationId, Guid employeeId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        var manager = organization.Users.FirstOrDefault(u => u.UserId == employeeId);
        if (manager is null)
            throw new NotFoundException<Organization>();

        organization.Users.Remove(manager);
    }

    public async Task<List<GetEmployeesView>> GetSellers(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users.Where(e => e.Role == ERole.Seller).ToList()).Adapt<List<GetEmployeesView>>();
    }

    public async Task<List<GetEmployeesView>> GetStaff(Guid organizationId)
    {
        var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
        if (organization is null)
            throw new NotFoundException<Organization>();

        return (organization.Users.ToList()).Adapt<List<GetEmployeesView>>();
    }
}
