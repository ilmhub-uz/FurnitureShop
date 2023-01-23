using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class CreateLikeDtoValidator : AbstractValidator<CreateLikeDto>
    {
        public CreateLikeDtoValidator() 
        {
            RuleFor(x => x.ProductId).NotNull().NotEqual(default(Guid));
        }
      
    }
}
