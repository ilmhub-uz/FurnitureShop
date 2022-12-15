using FurnitureShop.Api.ViewModel;

namespace FurnitureShop.Api.Dtos;

public class ProductCommentView
{
    public string? Comment { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public virtual List<ProductCommentView>? Children { get; set; }
    public virtual UserView? User { get; set; }

}
