using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Admin.Api.ViewModel
{
    public class ContractView
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public uint ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        public Dictionary<string, string>? ProductProperties { get; set; }
    }
}
