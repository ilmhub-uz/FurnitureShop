using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateCartDtoValidator : AbstractValidator<CreateCartProductDto>
{
    public CreateCartDtoValidator()
    {
        RuleFor(createCartDto => createCartDto.ProductId).NotEmpty();
        RuleFor(createCartDto => createCartDto.Count).NotEmpty();
        RuleFor(createCartDto => createCartDto.Properties).NotEmpty();
    }
}