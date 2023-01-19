using FurnitureShop.Merchant.Blazor.Dtos;
using FurnitureShop.Merchant.Blazor.Pages;
using FurnitureShop.Merchant.Blazor.ViewModel;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FurnitureShop.Merchant.Blazor.Services;

public class ProductService : HttpClientBase
{
    public ProductService(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<List<ProductView>?>> GetProductsAsync()
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Products");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var productsJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var products = JsonConvert.DeserializeObject<List<ProductView>>(productsJson);

            return new(true) { Data = products };
        }

        return new(false) { ErrorMessage = productsJson };
    }

    public async Task<Result<List<ProductView>?>> GetProductsAsync(string organizationId)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Products?OrganizationId={organizationId}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var productsJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var products = JsonConvert.DeserializeObject<List<ProductView>>(productsJson);

            return new(true) { Data = products };
        }

        return new(false) { ErrorMessage = productsJson };
    }
    public async Task<Result<ProductView>> GetProductByIdAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/Products/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var productJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var product = JsonConvert.DeserializeObject<ProductView>(productJson);

            return new(true) { Data = product };
        }

        return new(false) { ErrorMessage = productJson };
    }
    public async Task<Result> CreateProductAsync(CreateProductDto createProductDto)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/Products");
        httpRequest.Content = JsonContent.Create(createProductDto);

        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        var response = await httpClient.SendAsync(httpRequest);

        var createProductJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = createProductJson };
    }
    public async Task<Result> DeleteProductAsync(string id)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Products/{id}");
        httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var response = await httpClient.SendAsync(httpRequest);
        var deleteProductJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new(true);

        return new(false) { ErrorMessage = deleteProductJson };
    }
}
