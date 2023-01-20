using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class LoginUserdtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserdtoValidator()
        {
            RuleFor(loginDto => loginDto.UserName).NotNull().NotEmpty().MinimumLength(5)
                .MaximumLength(20);
            RuleFor(registeruserdto => registeruserdto.Password).NotNull().NotEmpty().MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
