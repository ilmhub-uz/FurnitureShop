using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class UpdateCategoryDto
{
    public string Name { get; set; }

    public int? ParentId { get; set; }
}