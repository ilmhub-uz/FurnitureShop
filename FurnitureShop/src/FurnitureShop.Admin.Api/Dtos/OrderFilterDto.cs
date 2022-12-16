using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class OrderFilterDto :PaginationParams
    {
        public Guid? OrganizationId { get; set; }
        public EOrderStatus? Status { get; set; }
    }
}
