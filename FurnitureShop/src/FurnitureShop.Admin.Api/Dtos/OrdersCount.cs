namespace FurnitureShop.Admin.Api.Dtos
{
    public class OrdersCount
    {
        public string Status { get; set; } = "Total Orders";
        public int TotalCount { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
