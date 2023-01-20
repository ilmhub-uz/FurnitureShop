using FurnitureShop.Merchant.Blazor.Dtos;
using FurnitureShop.Merchant.Blazor.Pages;
using FurnitureShop.Merchant.Blazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace FurnitureShop.Merchant.Blazor.Services;

public class EmployeeService : HttpClientBase
{
    public EmployeeService(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<List<GetEmployeeView>?>> GetStaffByIdAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Employee/allStaff/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var staffJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var staff = JsonConvert.DeserializeObject<List<GetEmployeeView>>(staffJson);

            return new(true) { Data = staff };
        }

        return new(false) { ErrorMessage = staffJson };
    }

    public async Task<Result<List<GetEmployeeView>?>> GetManagersByIdAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Employee/managers/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var managersJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var managers = JsonConvert.DeserializeObject<List<GetEmployeeView>>(managersJson);

            return new(true) { Data = managers };
        }

        return new(false) { ErrorMessage = managersJson };
    }

    public async Task<Result<List<GetEmployeeView>?>> GetSellersByIdAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Employee/sellers/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var sellersJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var sellers = JsonConvert.DeserializeObject<List<GetEmployeeView>>(sellersJson);

            return new(true) { Data = sellers };
        }

        return new(false) { ErrorMessage = sellersJson };
    }



    public async Task<Result> CreateEmployeeAsync(AddEmployeeDto addEmployeeDto)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/Employee");
        httpRequest.Content = JsonContent.Create(addEmployeeDto);

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        var response = await httpClient.SendAsync(httpRequest);

        var createEmployeeJson = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = createEmployeeJson };
    }

    public async Task<Result> DeleteEmployeeAsync(string organizaitonId, string employeeId)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Employee/{organizaitonId}/{employeeId}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var deleteEmployeeJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = deleteEmployeeJson };
    }
}
