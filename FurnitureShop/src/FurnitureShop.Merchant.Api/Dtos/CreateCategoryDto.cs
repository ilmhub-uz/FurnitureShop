using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;

public class CreateCategoryDto
{
    [Required]
    public string? Name { get; set; }

    public int? ParentId { get; set; }
}