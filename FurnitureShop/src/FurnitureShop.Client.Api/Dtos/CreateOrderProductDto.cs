using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Api.Dtos;

public class CreateOrderProductDto
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public uint Count { get; set; }
    public string? Properties { get; set; }
}