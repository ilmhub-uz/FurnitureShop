using FurnitureShop.Common.Helpers;
using FurnitureShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Merchant.Api.Helpers;

public static class ProductPagedListHelpers
{
    public static async Task<IEnumerable<T>> ToProductPagedListAsync<T>(this IQueryable<T> source, ProductPaganitionParams pageParams) where T : Product
    {
        if (pageParams.ProductName is not null)
            source = source.Where(x => x.Name!.Contains(pageParams.ProductName));

        if (pageParams.PriceAmounts is not null)
        {
            source = source.Where(x => x.Price >= pageParams.PriceAmounts.MinAmount && x.Price <= pageParams.PriceAmounts.MaxAmount);
            source = source.OrderByDescending(x => x.Price);
        }

        if (pageParams.Views && pageParams.Price)
            source = source.OrderByDescending(x => x.Price).ThenByDescending(x => x.Views);
        else
        {
            if (pageParams.Views)
                source = source.OrderByDescending(x => x.Views);

            if (pageParams.Price)
                source = source.OrderByDescending(x => x.Price);
        }

        if (pageParams.ReverseOrder)
            source = source.Reverse();

        return await source.ToPagedListAsync(pageParams);
    }

    public static IEnumerable<T> ToProductPageList<T>(this IEnumerable<T> source, ProductPaganitionParams pageParams) where T : Product
    {
        if (pageParams.ProductName is not null)
            source = source.Where(x => x.Name!.Contains(pageParams.ProductName));

        if (pageParams.PriceAmounts is not null)
        {
            source = source.Where(x => x.Price >= pageParams.PriceAmounts.MinAmount && x.Price <= pageParams.PriceAmounts.MaxAmount);
            source = source.OrderByDescending(x => x.Price);
        }

        if (pageParams.Views && pageParams.Price)
            source = source.OrderByDescending(x => x.Price).ThenByDescending(x => x.Views);
        else
        {
            if (pageParams.Views)
                source = source.OrderByDescending(x => x.Views);

            if (pageParams.Price)
                source = source.OrderByDescending(x => x.Price);
        }

        if (pageParams.ReverseOrder)
            source = source.Reverse();

        return source.ToPagedList(pageParams);
    }
}