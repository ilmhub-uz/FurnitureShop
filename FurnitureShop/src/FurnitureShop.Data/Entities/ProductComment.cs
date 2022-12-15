using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Data.Entities;

public class ProductComment
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }
    public Guid? ParentId { get; set; }
    public virtual ProductComment? Parent { get; set; }
    public virtual ICollection<ProductComment>? Children { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
