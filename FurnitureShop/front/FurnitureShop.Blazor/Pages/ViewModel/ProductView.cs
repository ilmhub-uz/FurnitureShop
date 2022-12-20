// using FurnitureShop.Data.Entities;

using FurnitureShop.Blazor.Shared;

namespace FurnitureShop.Blazor.Pages.ViewModel;

public class ProductView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    public decimal Price { get; set; }
    public bool OnTrend { get; set; }
    public bool OnSale { get; set; }
    public bool IsAvailable { get; set; }
    public uint Count { get; set; }
    public int Views { get; set; }
    public int Rate { get; set; }
    public virtual CategoryView? Category { get; set; }
    public virtual OrganizationView? Organization { get; set; }
}