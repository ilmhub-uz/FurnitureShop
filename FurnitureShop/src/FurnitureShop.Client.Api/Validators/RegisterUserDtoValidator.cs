using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator() 
        {
            RuleFor(registeruserdto => registeruserdto.UserName).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(registeruserdto => registeruserdto.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(registeruserdto => registeruserdto.Password).NotNull().NotEmpty().MinimumLength(6)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$")
                .WithMessage("password must contain at least one letter, one number and one special character.");
        }
    }
}
