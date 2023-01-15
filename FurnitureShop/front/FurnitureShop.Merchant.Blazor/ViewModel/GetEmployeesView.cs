namespace FurnitureShop.Merchant.Blazor.ViewModel;
public class GetEmployeesView
{
    public string UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }   
    public ERole Role { get; set; }
}
