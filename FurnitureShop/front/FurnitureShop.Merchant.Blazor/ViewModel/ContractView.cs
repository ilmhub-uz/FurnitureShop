namespace FurnitureShop.Admin.Blazor.ViewModel;
public class ContractView
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Dictionary<string, string>? ProductProperties { get; set; }
}
