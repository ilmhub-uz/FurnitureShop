using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;

public class SignInDtoValidator : AbstractValidator<LoginUserDto>
{
    public SignInDtoValidator()
    {
        RuleFor(loginDto => loginDto.UserName).NotNull().Length(5, 40).NotEmpty();
        RuleFor(loginDto => loginDto.Password).NotNull().Length(5, 20).NotEmpty();
    }
}