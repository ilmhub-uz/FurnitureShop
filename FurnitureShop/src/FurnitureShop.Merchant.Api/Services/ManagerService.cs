using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;
using FurnitureShop.Merchant.Api.ViewModel;
using JFA.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Services
{

    [Scoped]
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;

        public OrganizationService(IUnitOfWork unitOfWork, IEmailSender emailSender, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task AddManager(ClaimsPrincipal User, Guid organizationId, string email)
        {

            var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
            if (organization is null)
                throw new NotFoundException<Organization>();

            var joiner = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (joiner is null)
                throw new NotFoundException<AppUser>();

            var user = await _userManager.GetUserAsync(User);

            organization.Users.Add(new OrganizationUser()
            {
                UserId = joiner.Id,
                User = joiner,
                OrganizationId = organization.Id,
                Organization = organization,
                Role = ERole.Manager
            });

            var message = new Message(new string[] { $"{email}" }, "furnitureshop.uz organizations", $"{user.FirstName} has added you to {organization.Name} as manager. \n Congratulations 🎉");
            _emailSender.SendEmail(message);
        }

        public async Task<List<GetManagersView>> GetManagers(Guid organizationId)
        {
            var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
            if (organization is null)
                throw new NotFoundException<Organization>();

            return (organization.Users.ToList()).Adapt<List<GetManagersView>>();
        }

        public async Task RemoveManager(Guid organizationId, Guid managerId)
        {
            var organization = await _unitOfWork.Organizations.GetAll().FirstOrDefaultAsync(org => org.Id == organizationId);
            if (organization is null)
                throw new NotFoundException<Organization>();

            var manager = organization.Users.FirstOrDefault(u => u.UserId == managerId);
            if (manager is null)
                throw new NotFoundException<Organization>();

            organization.Users.Remove(manager);
        }

    }
}
