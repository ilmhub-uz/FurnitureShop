namespace FurnitureShop.Merchant.Api.Dtos
{
    public class RemoveEmployeeDto
    {
        public Guid OrganizationId { get; set; }
        public Guid EmployeeId { get; set; }    
    }
}
