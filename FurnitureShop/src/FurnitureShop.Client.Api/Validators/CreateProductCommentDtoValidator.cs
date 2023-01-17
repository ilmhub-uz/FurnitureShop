using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class CreateProductCommentDtoValidator : AbstractValidator<CreateProductCommentDto>
    {
       public CreateProductCommentDtoValidator() 
       {
            RuleFor(data => data.Comment).NotNull().WithMessage("comment kiritilishi kerak").NotEmpty();
       }
    }
}
