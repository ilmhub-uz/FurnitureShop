using FurnitureShop.Dashboard.Blazor.Dtos.Enums;
using FurnitureShop.Dashboard.Blazor.Models;
using System.ComponentModel.DataAnnotations;



namespace FurnitureShop.Dashboard.Blazor.Dtos;

public class ContractFilterDto : PaginationParams
{

    public Guid? OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? UserId { get; set; }
    public ESortStatus? SortStatus { get; set; }
    public EContractStatus? ContractStatus { get; set; }

}
