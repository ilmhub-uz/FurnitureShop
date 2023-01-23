using FurnitureShop.Merchant.Blazor.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Blazor.Dtos;

public class UpdateOrderDto
{
    [Required]
    public EOrderStatus Status { get; set; }
}