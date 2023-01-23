namespace FurnitureShop.Merchant.Blazor.Dtos;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}