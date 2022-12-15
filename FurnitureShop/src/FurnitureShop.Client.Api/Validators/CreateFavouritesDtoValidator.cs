using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators;

public class CreateFavouritesDtoValidator : AbstractValidator<CreateFavouriteDto>
{
	public CreateFavouritesDtoValidator()
	{
		RuleFor(createFavouritesDto => createFavouritesDto.ProductId).NotNull().WithMessage("ProductId is not valid");
	}
}

