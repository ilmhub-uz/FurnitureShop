using FurnitureShop.Admin.Api.Dtos;
using FurnitureShop.Admin.Api.ViewModel;

namespace FurnitureShop.Admin.Api.Services.Contracts;

public interface IOrganizationsService
{
    Task<List<OrganizationView>> GetOrganizationsAsync();
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
    Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto);
    Task DeleteOrganization(Guid organizationId, DeleteOrganizationDto deleteOrganizationDto);
}
