using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateCategoryDto
{
    [Required]
    public string? Name { get; set; }

    public int? ParentId { get; set; }
}