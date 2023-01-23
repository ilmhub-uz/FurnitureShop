namespace FurnitureShop.Merchant.Blazor.ViewModel;
public class ContractView
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Dictionary<string, string>? ProductProperties { get; set; }
}
