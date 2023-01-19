using FurnitureShop.Merchant.Blazor.Dtos;
using FurnitureShop.Merchant.Blazor.Pages;
using FurnitureShop.Merchant.Blazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace FurnitureShop.Merchant.Blazor.Services;

public class OrganizationService : HttpClientBase
{
    public OrganizationService(HttpClient httpClient) : base(httpClient)
    {

    }

    public async Task<Result<IEnumerable<OrganizationView>?>> GetOrganizationsAsync()
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Organizations");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        var response = await httpClient.SendAsync(httpRequest);
        var organizationsJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var organizations = JsonConvert.DeserializeObject<IEnumerable<OrganizationView>>(organizationsJson);

            return new(true) { Data = organizations };
        }

        return new(false) { ErrorMessage = organizationsJson };
    }
    public async Task<Result<OrganizationView>> GetOrganizationByIdAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Organizations/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        var response = await httpClient.SendAsync(httpRequest);
        var organziationJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var organization = JsonConvert.DeserializeObject<OrganizationView>(organziationJson);

            return new(true) { Data = organization };
        }

        return new(false) { ErrorMessage = organziationJson };
    }
    public async Task<Result> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/Organizations");
        httpRequest.Content = JsonContent.Create(createOrganizationDto);
        
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        var response = await httpClient.SendAsync(httpRequest);
        
        var createOrganziationJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = createOrganziationJson };
    }
    public async Task<Result> DeleteOrganizationAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Organizations/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        var response = await httpClient.SendAsync(httpRequest);
        var deleteOrganziationJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = deleteOrganziationJson };
    }

}
