using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;

public class SignInDtoValidator : AbstractValidator<LoginUserDto>
{
    public SignInDtoValidator()
    {
        RuleFor(loginDto => loginDto.UserName).NotNull().NotEmpty();
        RuleFor(loginDto => loginDto.Password).NotNull().NotEmpty();
    }
}