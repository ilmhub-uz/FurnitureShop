using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderDtoValidator()
    {
        RuleFor(createOrderDto => createOrderDto.User).NotNull();
        RuleFor(createOrderDto => createOrderDto.LastUpdatedAt).NotNull();
    }
}