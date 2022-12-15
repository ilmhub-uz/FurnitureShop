using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;
public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(productDto => productDto.Name).Length(1,50)
            .When(product => product.Name != null);
        
        RuleFor(productDto => productDto.Price).NotEmpty();
        
        RuleFor(productDto => productDto.CategoryId).NotEmpty();

        RuleFor(productDto => productDto.OrganizationId).NotEmpty();
    }
}
