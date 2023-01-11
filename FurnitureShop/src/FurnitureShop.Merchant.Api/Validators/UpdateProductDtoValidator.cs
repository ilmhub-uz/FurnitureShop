using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;
public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(productDto => productDto.Name).Length(1, 60)
            .NotNull().NotEmpty();

        RuleFor(productDto => productDto.Price).NotNull().ExclusiveBetween(0, 50000).WithMessage("Price must be between 0 and 50000");

        RuleFor(productDto => productDto.CategoryId).NotEmpty().NotNull();

        RuleFor(productDto => productDto.OrganizationId).NotEmpty().NotNull();

        RuleFor(productDto => productDto.Description).NotEmpty().NotNull().Length(10, 200);

        RuleFor(productDto => productDto.Count).NotEmpty().NotNull().InclusiveBetween(0u, 1000u);
    }
}