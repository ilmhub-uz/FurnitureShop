using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;
public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(categoryDto => categoryDto.Name).NotNull();
        RuleFor(categoryDto => categoryDto.Name).Length(3, 30).When(categoryDto => categoryDto.Name != null);
    }
}

