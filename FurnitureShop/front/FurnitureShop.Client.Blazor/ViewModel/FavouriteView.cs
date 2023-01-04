namespace FurnitureShop.Client.Api.ViewModel;

public class FavouriteView
{
    public virtual ICollection<FavouriteProductView>? FavouriteProducts { get; set; }
}