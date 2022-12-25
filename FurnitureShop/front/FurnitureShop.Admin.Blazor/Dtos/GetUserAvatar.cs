using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos
{
    public class GetUserAvatarDto
    {
        [Required] 
        public Guid UserId { get; set; }
    }
}
