using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Services;

public interface IOrganizationsService
{
    Task<List<OrganizationView>> GetOrganizationsAsync();
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
    Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto);
    Task DeleteOrganization(Guid organizationId, DeleteOrganizationDto deleteOrganizationDto);
}
