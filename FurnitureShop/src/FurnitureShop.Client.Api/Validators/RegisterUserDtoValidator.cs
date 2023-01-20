using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator() 
        {
            RuleFor(registeruserdto => registeruserdto.UserName).NotNull().NotEmpty().MinimumLength(5)
                .MaximumLength(20);
            RuleFor(registeruserdto => registeruserdto.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(registeruserdto => registeruserdto.Password).NotNull().NotEmpty().MinimumLength(6)
                .MaximumLength(20); ;
        }
    }
}
