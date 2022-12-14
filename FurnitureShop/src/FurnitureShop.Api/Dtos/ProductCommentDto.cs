using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Api.Dtos;

public class ProductCommentDto
{
    [Required]
    public string? Comment { get; set; }
    public Guid ParentId { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.Now;

}
