﻿namespace FurnitureShop.Merchant.Blazor.ViewModel;

public class ProductView
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? WithInstallation { get; set; }
    public string? Brend { get; set; }
    public string? Material { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    public decimal Price { get; set; }
    public virtual List<string>? Images { get; set; }
    public bool IsAvailable { get; set; }
    public uint Count { get; set; }
    public int Views { get; set; }
    public EProductStatus Status { get; set; }
    public int Rate { get; set; }
    public virtual CategoryView? Category { get; set; }
    public virtual string? OrganizationId { get; set; }
}