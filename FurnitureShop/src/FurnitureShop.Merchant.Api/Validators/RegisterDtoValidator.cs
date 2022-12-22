using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(registerDto => registerDto.UserName).NotNull().Length(5, 40).NotEmpty();
        RuleFor(registerDto => registerDto.FirstName).NotNull().Length(5, 20).NotEmpty();
        RuleFor(registerDto => registerDto.LastName).MaximumLength(40).MinimumLength(5);
        RuleFor(registerDto => registerDto.Password).NotNull().MaximumLength(20).MinimumLength(5);
    }
}