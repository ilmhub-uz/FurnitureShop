using FluentValidation;
using FurnitureShop.Client.Api.Dtos;
using FurnitureShop.Client.Api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Client.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IValidator<UpdateOrderDto> _createUserValidator;

    public OrdersController(IValidator<UpdateOrderDto> createUserValidator)
    {
        _createUserValidator = createUserValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok();
    }

    [HttpPut("{orderId}/cancel")]
    public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto,Guid orderId)
    {
        var result = _createUserValidator.Validate(updateOrderDto);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
}