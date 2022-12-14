using FurnitureShop.Api.ViewModel;
using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Api.Dtos;

public class ProductCommentView
{
    public string? Comment { get; set; }
    public Guid Id { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    public virtual List<ProductComment>? Children { get; set; }
    public virtual UserView? User { get; set; }

}
