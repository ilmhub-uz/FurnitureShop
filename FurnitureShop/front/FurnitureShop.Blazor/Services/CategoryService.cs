using FurnitureShop.Blazor.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;

namespace FurnitureShop.Blazor.Services
{
    public class CategoryService : HttpClientBase
    {
        public CategoryService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<CategoryView>?> GetCategories()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "api/categories");

            httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var categoriesJson = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryView>>(categoriesJson);
                return categories;
            }

            return null;
        }

    }
}
