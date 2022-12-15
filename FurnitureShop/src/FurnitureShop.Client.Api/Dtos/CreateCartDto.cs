using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Client.Api.Dtos;

public class CreateCartDto
{
    public Guid? ProductId { get; set; }
    public int? Count { get; set; }
    public string? Properties { get; set; }
}