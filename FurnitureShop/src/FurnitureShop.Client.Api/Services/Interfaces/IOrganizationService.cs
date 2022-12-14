using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync();
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
}