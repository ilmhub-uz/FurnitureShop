using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.Dtos
{
    public class GetManagersDto
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ERole Role { get; set; }
    }
}
