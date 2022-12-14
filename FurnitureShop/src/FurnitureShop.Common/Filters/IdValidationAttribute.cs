using FurnitureShop.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Common.Filters;
/// <summary>
/// This attribute validates if ids given to method parameters exist in database
/// In order the attribute to work properly, parameters should be given with entity names. For example: productId, orderId
/// </summary>
/// <exception cref="Microsoft.AspNetCore.Http.BadHttpRequestException"> </exception>
/// <exception cref="NotFoundException"></exception>
public class IdValidationAttribute : TypeFilterAttribute
{
    public IdValidationAttribute() : base(typeof(IdCheckerAttribute)){}
}