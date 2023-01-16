using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class LoginUserdtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserdtoValidator() 
        {
            RuleFor(loginDto => loginDto.UserName).NotNull().NotEmpty();
            RuleFor(loginDto => loginDto.Password).NotNull().NotEmpty();
        }
        
    }
}
