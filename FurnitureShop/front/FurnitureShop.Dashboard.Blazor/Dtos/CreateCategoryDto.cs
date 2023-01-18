using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Dashboard.Blazor.Dtos;
public class CreateCategoryDto
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
