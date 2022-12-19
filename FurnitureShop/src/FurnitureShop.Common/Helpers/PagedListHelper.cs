using FurnitureShop.Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FurnitureShop.Common.Helpers;

public static class PagedListHelper
{
    public static async Task<IEnumerable<T>> ToPagedListAsync<T>(this IQueryable<T> source, PaginationParams pageParams)
    {
        pageParams ??= new PaginationParams();

        HttpContextHelper.AddResponseHeader("X-Pagination",
            JsonConvert.SerializeObject(new PaginationMetaData(source.Count(), pageParams.Size, pageParams.Page)));

        return await source.Skip(pageParams.Size * (pageParams.Page - 1)).Take(pageParams.Size).ToListAsync();
    }

    public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams pageParams)
    {
        pageParams ??= new PaginationParams();

        HttpContextHelper.AddResponseHeader("X-Pagination",
            JsonConvert.SerializeObject(new PaginationMetaData(source.Count(), pageParams.Size, pageParams.Page)));

        return source.Skip(pageParams.Size * (pageParams.Page - 1)).Take(pageParams.Size).ToList();
    }
}