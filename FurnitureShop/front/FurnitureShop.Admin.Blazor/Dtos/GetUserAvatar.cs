using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos
{
    public class GetUserAvatar
    {
        [Required] 
        public Guid UserId { get; set; }
    }
}
