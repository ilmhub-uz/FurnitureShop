using FurnitureShop.Blazor.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;

namespace FurnitureShop.Blazor.Services
{
    public class OrganizationService : HttpClientBase
    {
        public OrganizationService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<OrganizationView>?> GetCategoriesAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "api/Organizations");

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

        public async Task<OrganizationView?> GetOrganizationByIdAsync(int organizationId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/Organizations/{organizationId}");

            httpRequest.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var organizationsJson = await response.Content.ReadAsStringAsync();
                var organizations = JsonConvert.DeserializeObject<OrganizationView>(organizationsJson);

                return organizations;
            }

            return null;
        }
    }
}
