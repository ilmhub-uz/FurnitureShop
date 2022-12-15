using FluentValidation;
using FurnitureShop.Data.Entities;

namespace FurnitureShop.Client.Api.Validators;

public class CreateProductCommentValidator : AbstractValidator<ProductComment>
{
    public CreateProductCommentValidator()
    {
        RuleFor(c => c.Comment).MinimumLength(1);
        RuleFor(c => c.Comment).MaximumLength(200);
        RuleFor(c => c.Comment).NotEmpty();
    }
}
