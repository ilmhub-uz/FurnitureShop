using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using static System.Net.WebRequestMethods;

namespace FurnitureShop.Common.Helpers;
public class RequestHelper<TModel> : IRequestHelper<TModel> where TModel : class
{
    private readonly HttpClient _httpClient;

    public RequestHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage?> SendRequestAsync(HttpMethod method, string baseUri, string? requestUri, TModel? model)
    {
        if (method is null)
            throw new ArgumentNullException(nameof(method));
        if (baseUri is null)
            throw new ArgumentNullException(nameof(baseUri));

        if (method == HttpMethod.Post)
            return await PostRequestAsync(baseUri, requestUri, model);

        if (method == HttpMethod.Get && model is not null)
        {
            return await GetRequestAsync<TModel>(baseUri, requestUri);
        }

        return await GetRequestAsync(baseUri, requestUri);
    }

    private async Task<HttpResponseMessage> GetRequestAsync(string baseUri, string? requestUri)
    {
        _httpClient.BaseAddress = new Uri(baseUri);
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        return await _httpClient.SendAsync(requestMessage);
    }
    private async Task<ResponseResult<T>?> GetRequestAsync<T>(string baseUri, string? requestUri)
    {
        _httpClient.BaseAddress = new Uri(baseUri);
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

        var responseMessage = await _httpClient.SendAsync(requestMessage);
        var result = await (responseMessage).Content.ReadFromJsonAsync<T>();

        var responseResult = new ResponseResult<T>()
        {
            Data = result,
            StatusCode = responseMessage.StatusCode
        };

        return responseResult;
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

public class ResponseResult<T>
{
    public T? Data {get; set;}
    public HttpStatusCode StatusCode { get; set; }
}