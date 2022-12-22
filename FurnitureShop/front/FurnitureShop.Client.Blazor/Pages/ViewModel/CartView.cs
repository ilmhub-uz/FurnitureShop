//using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Admin.Blazor.Shared;

public class CartView
{
    public virtual ICollection<CartProductView>? CartProducts { get; set; }
}