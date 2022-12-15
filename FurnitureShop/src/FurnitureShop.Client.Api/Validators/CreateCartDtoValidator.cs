using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateCartDtoValidator : AbstractValidator<CreateCartDto>
{
    public CreateCartDtoValidator()
    {
        RuleFor(createOrderDto => createOrderDto.ProductId).NotEmpty();
        RuleFor(createOrderDto => createOrderDto.Count).NotEmpty();
        RuleFor(createOrderDto => createOrderDto.Properties).NotEmpty();
    }
}