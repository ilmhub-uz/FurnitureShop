using FurnitureShop.Merchant.Blazor.Dtos;
using FurnitureShop.Merchant.Blazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection;

namespace FurnitureShop.Merchant.Blazor.Services;

public class OrganizationService : HttpClientBase
{
    public OrganizationService(HttpClient httpClient) : base(httpClient)
    {

    }

    public async Task<List<OrganizationView>?> GetOrganizationsAsync()
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Organizations");

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);

        if (response.IsSuccessStatusCode)
        {
            var organizationsJson = await response.Content.ReadAsStringAsync();
            var organizations = JsonConvert.DeserializeObject<List<OrganizationView>>(organizationsJson);

            return organizations;
        }

        return null;
    }
    public async Task<OrganizationView?> GetOrganizationByIdAsync(Guid id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Organizations/{id}");

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);

        if (response.IsSuccessStatusCode)
        {
            var organziationJson = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<OrganizationView>(organziationJson);

            return product;
        }

        return null;
    }
    public async Task<bool> CreateOrganizationAsync(CreateOrganizationDto? createOrganizationDto)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/Organizations");
        httpRequest.Content = JsonContent.Create(createOrganizationDto);

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
    public async Task<bool> DeleteOrganizationAsync(Guid id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Organizations/{id}");

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

}
