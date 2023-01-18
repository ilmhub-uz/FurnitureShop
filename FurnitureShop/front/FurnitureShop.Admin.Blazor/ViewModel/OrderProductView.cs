

namespace FurnitureShop.Admin.Blazor.ViewModel;

public class OrderProductView
{

    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }
    public uint Count { get; set; }
    public string? Properties { get; set; }
}