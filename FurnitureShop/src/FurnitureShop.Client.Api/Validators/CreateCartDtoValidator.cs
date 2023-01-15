using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateCartDtoValidator : AbstractValidator<CreateCartProductDto>
{
    public CreateCartDtoValidator()
    {
        RuleFor(createCartDto => createCartDto.ProductId).NotNull()
            .NotEqual(default(Guid));

        RuleFor(createCartDto => createCartDto.Count).NotEmpty()
            .NotEqual(default(uint)).When(c => c.Count > 0)
            .WithMessage("The number entered must be greater than 0");

        RuleFor(createCartDto => createCartDto.Properties).NotEmpty();
    }
}