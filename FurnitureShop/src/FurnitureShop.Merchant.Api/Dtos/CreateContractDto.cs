using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Merchant.Api.Dtos;
public class CreateContractDto
{
    public Guid? OrderId { get; set; }
}