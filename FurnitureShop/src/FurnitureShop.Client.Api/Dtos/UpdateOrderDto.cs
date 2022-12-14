using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateOrderDto
{
    public EOrderStatus Status { get; set; }
}