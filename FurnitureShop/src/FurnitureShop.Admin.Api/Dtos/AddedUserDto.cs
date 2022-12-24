using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class AddedUserDto
    {
        public string? FirstName { get; set; }
        public string? Password { get; set; }
        EUserStatus? Status { get; set; }
    }
}
