using FurnitureShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Common.Filters;
public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(EPermission permission = EPermission.None) : base(typeof(AuthorizePermissionAttribute))
    {  
        Arguments = new object[] { permission };
    }
}