using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Dtos
{
    public class OrderFilterDto
    {
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        public EOrderStatus? OrderStatus { get; set; }
    }
}
