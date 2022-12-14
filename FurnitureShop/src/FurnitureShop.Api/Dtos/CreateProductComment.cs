using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Api.Dtos;

public class CreateProductComment
{
    [Required]
    public string? Comment { get; set; }
    public Guid? ParentId { get; set; }

}
