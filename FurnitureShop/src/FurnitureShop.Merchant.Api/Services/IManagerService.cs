using FurnitureShop.Merchant.Api.ViewModel;
using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Services
{
    public interface IManagerService
    {
        Task AddManager(ClaimsPrincipal User, Guid organizationId, string email);
        Task<List<GetManagersView>> GetManagers(Guid organizationId);
        Task RemoveManager(Guid organizationId, Guid managerId);
    }
}
