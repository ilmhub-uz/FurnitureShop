using FurnitureShop.Common.Models;

namespace FurnitureShop.Admin.Api.Dtos
{
    public class ContractsFilterDto: PaginationParams
    {
        public Guid? ProductId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
