using FurnitureShop.Admin.Blazor.Dtos.Enums;
using FurnitureShop.Admin.Blazor.Models;
using System.ComponentModel.DataAnnotations;


namespace FurnitureShop.Admin.Blazor.Dtos;

public class ContractFilterDto : PaginationParams
{
   
   public Guid? OrderId { get; set; }
   public Guid? ProductId { get; set; }
   public Guid? UserId { get; set; }
   public ESortStatus? SortStatus { get; set; }
   public EContractStatus? ContractStatus { get; set; }
   
}
