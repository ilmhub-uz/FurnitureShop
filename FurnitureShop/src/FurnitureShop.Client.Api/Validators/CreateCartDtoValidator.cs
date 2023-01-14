using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateCartDtoValidator : AbstractValidator<CreateCartProductDto>
{
    public CreateCartDtoValidator()
    {
        RuleFor(createCartDto => createCartDto.ProductId).NotEmpty();

        RuleFor(createCartDto => createCartDto.Count).NotEmpty()
            .When(c => c.Count > 0)
            .WithMessage("The number entered must be greater than 0");

        RuleFor(createCartDto => createCartDto.Properties).NotEmpty();
    }
}