using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateProductCommentDto
{
    public string? Comment { get; set; }
    public Guid? ParentId { get; set; }
}
