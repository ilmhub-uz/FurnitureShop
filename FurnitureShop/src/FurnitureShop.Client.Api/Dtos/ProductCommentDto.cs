namespace FurnitureShop.Client.Api.Dtos;

public class ProductCommentDto
{
    public Guid Id { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ParentId { get; set; }
}