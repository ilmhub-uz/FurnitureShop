using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(createOrderDto => createOrderDto.UserId).NotEmpty();
        
        RuleFor(createOrderDto => createOrderDto.OrganizationId).NotEmpty(); 

        RuleFor(ct => ct.CartProductIds).NotNull()
            .WithMessage("CartProduct is not null");
    }
}