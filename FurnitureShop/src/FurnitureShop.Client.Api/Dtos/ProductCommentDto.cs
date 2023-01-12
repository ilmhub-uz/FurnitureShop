namespace FurnitureShop.Client.Api.Dtos;

public class ProductCommentDto
{
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid? ParentId { get; set; }
}