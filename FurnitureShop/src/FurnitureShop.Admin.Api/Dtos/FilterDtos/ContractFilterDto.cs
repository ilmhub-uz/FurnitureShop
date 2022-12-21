﻿using FurnitureShop.Admin.Api.Dtos.Enums;
using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos.FilterDtos
{
    public class ContractFilterDto : PaginationParams
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
        public ESortStatus? SortStatus { get; set; }
        public EContractStatus? ContractStatus { get; set; }

    }
}
