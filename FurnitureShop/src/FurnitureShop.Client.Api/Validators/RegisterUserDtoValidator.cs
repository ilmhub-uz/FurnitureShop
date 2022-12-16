using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
	public RegisterUserDtoValidator()
	{
		RuleFor(registerUserDto => registerUserDto.UserName).Length(10, 128).NotNull().WithMessage("Username is not valid");
		RuleFor(registerUserDto => registerUserDto.FirstName).Length(10, 128).NotNull().WithMessage("Firstname is not valid");
		RuleFor(registerUserDto => registerUserDto.Password).Length(10, 128).Matches(@"[a-z]+").Matches(@"0-9").WithMessage("Password must contain at least one lowercase letter and one digit.");
		RuleFor(registerUserDto => registerUserDto.Email).EmailAddress().NotEmpty().WithMessage("Email is not valid");
	}
}
