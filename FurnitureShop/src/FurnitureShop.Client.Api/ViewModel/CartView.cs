using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Client.Api.ViewModel;

public class CartView
{
    public virtual ICollection<CartProductView>? CartProducts { get; set; }
}