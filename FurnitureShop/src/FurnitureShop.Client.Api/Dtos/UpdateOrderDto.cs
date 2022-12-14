using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using FurnitureShop.Client.Api.ViewModel;

namespace FurnitureShop.Client.Api.Dtos;

public class UpdateOrderDto
{

    public virtual Organization? Organization { get; set; }

    public virtual AppUser? User { get; set; }// dto user kere boladimmi

    public EOrderStatus Status { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public virtual ICollection<OrderProductView>? OrderProducts { get; set; }
}