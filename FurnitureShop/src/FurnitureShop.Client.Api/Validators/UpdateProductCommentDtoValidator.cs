using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class UpdateProductCommentDtoValidator : AbstractValidator<UpdateProductCommentDto>
    {
        public UpdateProductCommentDtoValidator()
        {
            RuleFor(data => data.Comment).NotNull().NotEmpty();
        }
    }
}
