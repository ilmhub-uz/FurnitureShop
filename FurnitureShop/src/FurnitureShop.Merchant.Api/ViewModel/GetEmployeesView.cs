using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.ViewModel
{
    public class GetEmployeesView
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }   
        public ERole Role { get; set; }
    }
}
