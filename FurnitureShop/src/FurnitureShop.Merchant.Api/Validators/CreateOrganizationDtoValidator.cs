using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;
public class CreateOrganizationDtoValidator : AbstractValidator<CreateOrganizationDto>
{
    public CreateOrganizationDtoValidator()
        => RuleFor(organizationDto => organizationDto.Name).Length(1,15)
        .When(organizationDto => organizationDto.Name != null);
}
