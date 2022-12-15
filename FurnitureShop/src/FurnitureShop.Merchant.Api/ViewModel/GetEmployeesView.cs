using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.ViewModel
{
    public class GetEmployeesView
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ERole Role { get; set; }
    }
}
