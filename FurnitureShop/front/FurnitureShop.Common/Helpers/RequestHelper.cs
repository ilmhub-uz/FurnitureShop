using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace FurnitureShop.Common.Helpers;
public class RequestHelper<TModel> : IRequestHelper<TModel> where TModel : class
{
    private readonly HttpClient _httpClient;

    public RequestHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string baseUri, string? requestUri, TModel? model)
    {
        if (method is null)
            throw new ArgumentNullException(nameof(method));
        if (baseUri is null)
            throw new ArgumentNullException(nameof(baseUri));

        if (method == HttpMethod.Post)
            return await PostRequestAsync(baseUri, requestUri, model);
        
        return await GetRequestAsync(baseUri, requestUri);
    }

    private async Task<HttpResponseMessage> GetRequestAsync(string baseUri, string? requestUri)
    {
        _httpClient.BaseAddress = new Uri(baseUri);
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        return await _httpClient.SendAsync(requestMessage);
    }

    private async Task<HttpResponseMessage> PostRequestAsync(string baseUri, string? requestUri, TModel? model)
    {
        _httpClient.BaseAddress = new Uri(baseUri);
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        
        requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        if (model is not null)
            requestMessage.Content = JsonContent.Create(model);

        return await _httpClient.SendAsync(requestMessage);
    }
}