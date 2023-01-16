using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Admin.Blazor.Dtos;
public class CreateCategoryDto
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
