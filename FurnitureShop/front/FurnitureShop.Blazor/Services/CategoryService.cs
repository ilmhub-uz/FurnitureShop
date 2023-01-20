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

        public async Task<List<CategoryView>?> GetCategoriesAsync()
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
        
        public async Task<CategoryView?> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"$api/categories/{categoryId}");

                httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

                var response = await httpClient.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    var categoryJson = await response.Content.ReadAsStringAsync();
                    var category = JsonConvert.DeserializeObject<CategoryView>(categoryJson);

                    return category;
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
