using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.ViewModel;

public class ContractView
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public EContractStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime FinishDate { get; set; }
    public Guid OrderId { get; set; }
}
