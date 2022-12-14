using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Blazor.Dtos;

public class CreateCategoryDto
{
    [Required]
    public string? Name { get; set; }

    public int? ParentId { get; set; }
}