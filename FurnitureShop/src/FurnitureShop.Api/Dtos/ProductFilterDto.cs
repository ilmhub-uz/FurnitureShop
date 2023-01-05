using FurnitureShop.Common.Models;

namespace FurnitureShop.Api.Dtos;

public class ProductFilterDto : PaginationParams
{
    public bool? WithInstallation { get; set; }
    public List<string>? Brends { get; set; }
    public List<string>? Materials { get; set; }
    public int? FromPrice { get; set; }
    public int? ToPrice { get; set; }
    public bool? IsAvailable { get; set; }
    public List<int>? CategoriesId { get; set; }
    public List<Guid>? OrganizationsId { get; set; }
    public ESortStatus SortingStatus { get; set; } 
}

public enum ESortStatus
{
    Popular,
    Cheap,
    Expensive,
    Rate
}