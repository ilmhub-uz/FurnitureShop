namespace FurnitureShop.Merchant.Api.Dtos;

public class PriceAmounts
{
    public uint MinAmount { get; set; } = uint.MinValue;
    public uint MaxAmount { get; set; } = uint.MaxValue;
}
