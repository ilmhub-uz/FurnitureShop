using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Dtos
{
    public class ProductFilterDto : PaginationParams
    {
        public Guid? OrganizationId { get; set; }
        public int? CategoryId { get; set; }
        public uint? Price { get; set; }
        public string? Brend { get; set; }
        public uint? Rate { get; set; }
        public DateTime? DateTime { get; set; }
        public EProductSorting? ProductSorting { get; set; }
        public EProductStatus? ProductStatus { get; set; }
    }
}
