using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.Dtos
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
