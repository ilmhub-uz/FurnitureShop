using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
	public CreateRoleDtoValidator()
	{
        RuleFor(roleDto => roleDto.Name).NotNull();
        RuleFor(roleDto => roleDto.Name).Length(3, 30).When(roleDto => roleDto.Name != null);
    }
}