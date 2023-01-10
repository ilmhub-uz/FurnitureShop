using FurnitureShop.Common.Models;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Dtos
{
    public class OrderFilterDto : PaginationParams
    {
        public Guid? OrganizationId { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public EOrderStatus? OrderStatus { get; set; }
    }
}
