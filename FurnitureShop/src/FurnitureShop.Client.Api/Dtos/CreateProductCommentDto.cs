using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class CreateProductCommentDto
{
    public string? Comment { get; set; }
    public Guid? ParentId { get; set; }
}


