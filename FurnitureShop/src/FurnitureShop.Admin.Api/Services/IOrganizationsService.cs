using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Services;

public interface IOrganizationsService
{
    Task<List<OrganizationView>> GetOrganizationsAsync(OrganizationFilterDto filter);
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
    Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto);
    Task DeleteOrganization(Guid organizationId);
}
