using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Repositories;

namespace FurnitureShop.Api.Services;

public class SearchService
{
    private readonly IUnitOfWork unitOfWork;
    public SearchService(IUnitOfWork _unitOfWork) => unitOfWork = _unitOfWork;

    public List<Product> SearchbyName(string name)
    {
        var _name = name.FormatName();
        var products = unitOfWork.Products.GetAll()
                     .Where(p => p.Name.FormatName().Contains(_name)).ToList();
        return products;
    }
}

public static class FormatString 
{
    public static string FormatName(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c) && Char.IsLetterOrDigit(c))
            .ToArray()).ToLower();
    }
}