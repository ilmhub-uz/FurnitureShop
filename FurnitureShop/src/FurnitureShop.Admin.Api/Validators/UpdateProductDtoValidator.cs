using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(dto => dto.CategoryId).NotNull();
        RuleFor(dto => dto.CategoryId).GreaterThan(0);
        RuleFor(dto => dto.OrganizationId).NotEmpty();
        RuleFor(dto => dto.Status).NotEmpty();
    }
}