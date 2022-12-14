using System.Security.Claims;

namespace FurnitureShop.Merchant.Api.Services
{
    public interface IManagerService
    {
        Task AddManager(ClaimsPrincipal User, Guid organizationId, string email);
        Task<List<GetManagersDto>> GetManagers(Guid organizationId);
        Task RemoveManager(Guid organizationId, Guid managerId);
    }
}
