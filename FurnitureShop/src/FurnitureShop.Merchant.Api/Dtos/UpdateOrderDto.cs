using System.ComponentModel.DataAnnotations;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Merchant.Api.Dtos;

public class UpdateOrderDto
{
    [Required]
    public EOrderStatus Status { get; set; }
    [Required]
    public DateTime FinishDate {get; set;}
}