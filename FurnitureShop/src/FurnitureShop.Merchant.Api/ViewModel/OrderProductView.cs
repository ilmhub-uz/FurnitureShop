﻿namespace FurnitureShop.Merchant.Api.ViewModel;

public class OrderProductView
{
    public Guid Id { get; set; }
    public virtual ProductView? Product { get; set; }
    public uint Count { get; set; }
    public string? Properties { get; set; }
}