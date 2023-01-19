using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().When(x => string.IsNullOrEmpty(x.FirstName)
                                               && string.IsNullOrEmpty(x.LastName)
                                               && string.IsNullOrEmpty(x.Avatar));
            RuleFor(x => x.FirstName).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)
                                                       && string.IsNullOrEmpty(x.LastName)
                                                       && string.IsNullOrEmpty(x.Avatar));
            RuleFor(x => x.LastName).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)
                                                       && string.IsNullOrEmpty(x.FirstName)
                                                       && string.IsNullOrEmpty(x.Avatar));
            RuleFor(x => x.Avatar).NotEmpty().When(x => string.IsNullOrEmpty(x.UserName)
                                                    && string.IsNullOrEmpty(x.FirstName)
                                                    && string.IsNullOrEmpty(x.LastName));
        }
    }
}
