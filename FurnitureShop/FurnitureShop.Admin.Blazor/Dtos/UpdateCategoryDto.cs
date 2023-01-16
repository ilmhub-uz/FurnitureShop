using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Admin.Blazor.Dtos;

public class UpdateCategoryDto
{
    public string Name { get; set; }

    public int? ParentId { get; set; }
}