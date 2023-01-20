using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using FurnitureShop.Blazor.Pages.ViewModel;
using FurnitureShop.Blazor.Shared;

namespace FurnitureShop.Blazor.Services
{
    public class ProductService : HttpClientBase
    {
        public ProductService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<ProductView>?> GetProductsAsync()
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, "api/products");

                httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await httpClient.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    var productsJson = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<ProductView>>(productsJson);

                    return products;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public async Task<ProductView?> GetProductByIdAsync(Guid id)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/products/{id}");

                httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await httpClient.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    var productJson = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<ProductView>(productJson);

                    return product;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
