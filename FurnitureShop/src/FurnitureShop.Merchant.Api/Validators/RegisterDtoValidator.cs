using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(registerDto => registerDto.UserName).NotNull().Length(5, 40).NotEmpty().Must(x =>
                (Char.IsDigit(x[0]) || x[0] == '_') == false)
            .WithMessage("'{ PropertyName}' must not start with numeric or special characters")
            .Matches(@"^[a-zA-Z0-9_]*$")
            .WithMessage("'{ PropertyName}' must contain one or more special characters.");
        RuleFor(registerDto => registerDto.FirstName).NotNull().Length(3, 20).NotEmpty().Matches("^[a-zA-Z]*$")
            .WithMessage("FirstName must contain only alphabetical characters.");
        RuleFor(registerDto => registerDto.LastName).NotNull().Length(3, 20).NotEmpty().Matches("^[a-zA-Z]*$")
            .WithMessage("LastName must contain only alphabetical characters.");
        RuleFor(registerDto => registerDto.Password).NotNull().MaximumLength(40).MinimumLength(5).WithMessage("Password length must be between 5 and 40");
        RuleFor(registerDto => registerDto.Email).EmailAddress();
    }
}