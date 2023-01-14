using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateProductCommentDto
{
    [Required]
    public string? Comment { get; set; }
}
