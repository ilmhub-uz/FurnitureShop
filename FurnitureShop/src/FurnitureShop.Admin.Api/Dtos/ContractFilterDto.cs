using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class ContractFilterDto : PaginationParams
    {
       public ESortStatus Status { get; set; }
    }
}
