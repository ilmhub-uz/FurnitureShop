using FurnitureShop.Merchant.Blazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;

namespace FurnitureShop.Merchant.Blazor.Services
{
    public class EmployeeService : HttpClientBase
    {
        public EmployeeService(HttpClient httpClient) : base(httpClient) { }

        public async Task<List<UserView>> GetEmployees(string organizationId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:1009");
            var message = new HttpRequestMessage();
            message = new HttpRequestMessage(HttpMethod.Get, $"/api/employee/allstaff/{organizationId}");
            message.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var json = await (await client.SendAsync(message)).Content.ReadAsStringAsync();
            var employees = JsonConvert.DeserializeObject<List<UserView>>(json);
            return employees;
        }
    }
}
