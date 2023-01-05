using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateOrderDto
{
    public EOrderStatus Status { get; set; }
}