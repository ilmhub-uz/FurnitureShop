//using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Blazor.ViewModel;

public class CartView
{
    public virtual ICollection<CartProductView>? CartProducts { get; set; }
}