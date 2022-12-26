using FurnitureShop.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Services;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync(PaginationParams paginationParams);
    OrganizationView GetOrganizationById(Guid organizationId);
}