//using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Blazor.Shared;

public class CartView
{
    public virtual ICollection<CartProductView>? CartProducts { get; set; }
}