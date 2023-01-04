
namespace FurnitureShop.Client.Api.ViewModel;

public class CartView
{
    public virtual ICollection<CartProductView>? CartProducts { get; set; }
}