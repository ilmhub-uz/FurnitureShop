using FurnitureShop.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureShop.Admin.Api.ViewModel;

public class OrderProductView
{

    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }
    public uint Count { get; set; }
    public string? Properties { get; set; }
}