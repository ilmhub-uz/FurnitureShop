using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;

public class CreateOrganizationDtoValidator:AbstractValidator<CreateOrganizationDto>
{
    public CreateOrganizationDtoValidator()
    {
        RuleFor(orgnizatioDto => orgnizatioDto.Name).NotNull();
        RuleFor(organizationDto => organizationDto.Name).Length(3,30)
                                                        .When(organizationDto => organizationDto.Name != null);
    }
}