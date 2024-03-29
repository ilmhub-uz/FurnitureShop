﻿namespace FurnitureShop.Merchant.Api.ViewModel;
public class ContractView
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public uint ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public Dictionary<string, string>? ProductProperties { get; set; }
}