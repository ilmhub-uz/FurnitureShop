using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.ViewModel;

public class ContractView
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }

    public Guid OrderId { get; set; }
    public EContractStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FinishDate { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Dictionary<string, string>? ProductProperties { get; set; }
}
