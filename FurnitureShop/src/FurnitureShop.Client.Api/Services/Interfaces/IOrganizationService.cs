using FurnitureShop.Client.Api.ViewModel;
using FurnitureShop.Common.Models;

namespace FurnitureShop.Client.Api.Services.Interfaces;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync();
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
    Task<List<OrganizationProductsView>> GetOrganizationProducts(Guid organizationId, PaginationParams paginationParams);
}