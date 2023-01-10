using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync(OrganizationFilterDto filterDto);
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
}