using FluentValidation;
using FluentValidation.Validators;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;

public class UpdateOrganizationValidator:AbstractValidator<UpdateOrganizationDto>
{
    public UpdateOrganizationValidator()
    {
        RuleFor(updateorganizationDto => updateorganizationDto.Name).NotNull();
        RuleFor(updateorganizationDto => updateorganizationDto.Name).Length(3, 30)
                                                                    .When(updateorganizationDto => updateorganizationDto.Name != null);
        RuleFor(updateorganizationDto => updateorganizationDto.Status).NotNull();
        RuleFor(updateorganizationDto => updateorganizationDto.Status).IsInEnum();
    }
}


