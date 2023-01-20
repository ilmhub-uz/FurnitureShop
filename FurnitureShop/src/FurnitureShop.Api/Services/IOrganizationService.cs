using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Services;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync(PaginationParams paginationParams);
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
}