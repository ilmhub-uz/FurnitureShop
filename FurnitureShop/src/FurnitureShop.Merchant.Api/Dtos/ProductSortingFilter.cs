﻿using FurnitureShop.Common.Models;
using FurnitureShop.Merchant.Api.Dtos.Enums;

namespace FurnitureShop.Merchant.Api.Dtos;
public class ProductSortingFilter : PaginationParams
{
    public Guid? OrganizationId { get; set; }
    public decimal? Price { get; set; }
    public ESortingParameters? SortingParams { get; set; }
    public string? Brend { get; set; }

}
