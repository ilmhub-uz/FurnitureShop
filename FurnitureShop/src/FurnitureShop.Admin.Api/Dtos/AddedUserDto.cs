using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class AddedUserDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public EUserStatus Status { get; set; }

    }
}
