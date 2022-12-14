using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;
public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(categoryDto => categoryDto.Name).NotNull();
        RuleFor(categoryDto => categoryDto.Name).Length(3, 30).When(categoryDto => categoryDto.Name != null);
    }
}

