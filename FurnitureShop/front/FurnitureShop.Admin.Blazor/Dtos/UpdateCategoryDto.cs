using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.Dtos;

public class UpdateCategoryDto
{
    public string Name { get; set; }

    public int? ParentId { get; set; }
}