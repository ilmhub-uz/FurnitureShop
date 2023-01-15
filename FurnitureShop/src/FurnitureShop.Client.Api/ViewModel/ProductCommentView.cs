namespace FurnitureShop.Client.Api.ViewModel;

public class ProductCommentView
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public List<ProductCommentView>? Children { get; set; }
    public DateTime CreatedAt { get; set; }
}