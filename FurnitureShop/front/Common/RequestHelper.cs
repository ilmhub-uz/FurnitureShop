using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net;
using System.Net.Http.Json;

namespace Common
{
    public class RequestHelper : IRequestHelper
    {
        public async Task<HttpStatusCode> SendRequest<TModel>(string baseUri, string requestUri, TModel model)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            var message = new HttpRequestMessage(HttpMethod.Post, requestUri);
            message.Content = JsonContent.Create(model);
            message.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var responseMessage = await client.SendAsync(message);
            var responseStatusCode = responseMessage.StatusCode;

            return responseStatusCode;
        }
    }
}
