using FurnitureShop.Merchant.Api.Dtos;

public class ChangeOrderStatusDto
{
    public EOStatus eOStatus { get; set; }
    public DateTime finishDate { get; set; }
}