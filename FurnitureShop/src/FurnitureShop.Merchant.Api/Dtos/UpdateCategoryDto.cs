using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class UpdateCategoryDto
{
    [Required]
    public string? Name { get; set; }

    public int? ParentId { get; set; }
}