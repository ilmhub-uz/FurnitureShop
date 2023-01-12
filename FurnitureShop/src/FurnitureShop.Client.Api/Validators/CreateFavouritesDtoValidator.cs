using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateFavouritesDtoValidator : AbstractValidator<CreateFavouriteDto>
{
    public CreateFavouritesDtoValidator()
    {
        RuleFor(favouriteDto => favouriteDto.ProductId).NotEmpty()
            .WithMessage("Product id can not be empty")
            .NotNull().
            WithMessage("Product id can not be null");
    }
}
