using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.ViewModel;

public class OrganizationUserView
{
    public Guid UserId { get; set; }
    
    public Guid OrganizationId { get; set; }

    public ERole Role { get; set; }
}
