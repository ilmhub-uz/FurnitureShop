using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Blazor.Dtos
{
    public class GetUserAvatarDto
    {
        [Required] 
        public Guid UserId { get; set; }
    }
}
