using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Common;

public interface IRequestHelper
{
    public Task<HttpStatusCode> SendRequest<TModel>(string baseUri, string requestUri, TModel model);

}