using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.ViewModel
{
    public class GetEmployeesView
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ERole Role { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
