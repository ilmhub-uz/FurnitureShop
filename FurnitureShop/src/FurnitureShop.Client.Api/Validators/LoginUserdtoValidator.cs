using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class LoginUserdtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserdtoValidator()
        {
            RuleFor(loginDto => loginDto.UserName).NotNull().NotEmpty();
            RuleFor(registeruserdto => registeruserdto.Password).NotNull().NotEmpty().MinimumLength(6)
                 .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")
                 .WithMessage("password must contain at least one letter, one number and one special character.");
        }

    }
}
