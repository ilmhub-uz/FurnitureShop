using FurnitureShop.Common.Exceptions;
using FurnitureShop.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using FurnitureShop.Data.Repositories;

namespace FurnitureShop.Common.Filters;

public class IdCheckerAttribute : ActionFilterAttribute
{
    private readonly AppDbContext _context;

    private readonly string _notFoundExceptionMessage = "One or more objects has not been found.";

    private readonly string _noParametersFoundExceptionMessage =
        "Error! Any parameters about ID have not been found";

    private readonly string _notFoundRepositoryExceptionMessage = "Repository based on given parameter has not been found!";

    private const string _targetClass = "Repository";

    public IdCheckerAttribute(AppDbContext context)
    {
        _context = context;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var parameters = context.ActionArguments
            .Where(kvp => kvp.Key.ToLower().Contains("id"))
            .ToDictionary(x => x.Key, x => x.Value);
        if (parameters.Keys.Count <= 0)
        {
            throw new BadHttpRequestException(_noParametersFoundExceptionMessage);
            return;
        }

        var normalizedParameters = Normalizer(parameters);

        if (normalizedParameters.Any(kvp => FindTypeByName(kvp.Key + _targetClass) is null))
        {
            throw new BadHttpRequestException(_notFoundRepositoryExceptionMessage);
            return;
        }

        var classTypes = new Dictionary<Type, object?>();

        foreach (var kvp in normalizedParameters)
            classTypes.Add(FindTypeByName(kvp.Key + _targetClass), kvp.Value);

        var results = new List<bool>();

        foreach (var classTypeKeyValuePair in classTypes)
        {
            var instance = (IEntityExistsRepository)Activator.CreateInstance(classTypeKeyValuePair.Key, _context)!;
            var id = classTypeKeyValuePair.Value;

            results.Add(await instance.IsExists(id));
        }

        if (results.Any(b => b == false))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            throw new NotFoundException(_notFoundExceptionMessage);
            return;
        }

        await next();
    }

    private Dictionary<string, object?> Normalizer(IDictionary<string, object?> parameters)
    {
        var normalizedKeys = parameters.Keys.Select(key => key[..^2]);
        var values = parameters.Values;

        var mappedDictionary = normalizedKeys.Zip(values, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);

        return mappedDictionary;
    }
    private Type? FindTypeByName(string entityName) =>
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.IsClass && t.Name.ToLower() == entityName.ToLower());
}