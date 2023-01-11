using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;
public class CreateOrganizationDtoValidator : AbstractValidator<CreateOrganizationDto>
{
    public CreateOrganizationDtoValidator()
    {
        RuleFor(organizationDto => organizationDto.Name).Length(1, 30).NotNull().NotEmpty().Matches("^[a-zA-Z']*$")
            .WithMessage("Organization name must contain only alphabetical characters.");
    }
}
