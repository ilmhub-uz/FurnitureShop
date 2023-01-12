using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateProductCommentValidator : AbstractValidator<ProductCommentDto>
{
    public CreateProductCommentValidator()
    {
        RuleFor(commentDto => commentDto.Comment)
            .NotNull().WithMessage("Comment can not be null")
            .NotEmpty().MinimumLength(1).WithMessage("Comment can not be empty");

        RuleFor(commentDto => commentDto.Id)
            .NotNull().NotEmpty().WithMessage("Comment id can not be null or empty");

        RuleFor(commentDto => commentDto.ProductId)
            .NotNull().NotEmpty().WithMessage("Product id can not be null or empty");

        RuleFor(commentDto => commentDto.UserId)
            .NotNull().NotEmpty().WithMessage("User id can not be null or empty");
    }
}
