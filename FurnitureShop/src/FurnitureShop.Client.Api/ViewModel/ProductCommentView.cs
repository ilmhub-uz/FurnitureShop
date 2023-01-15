namespace FurnitureShop.Client.Api.ViewModel;

public class ProductCommentView
{
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public List<ProductCommentView>? Children { get; set; }
}