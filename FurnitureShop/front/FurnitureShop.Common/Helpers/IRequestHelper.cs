namespace FurnitureShop.Common.Helpers;
public interface IRequestHelper<in TModel> where TModel : class
{
    Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string baseUri, string? requestUri, TModel? model);
}