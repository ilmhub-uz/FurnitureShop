using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators;
public class CreateContractDtoValidator : AbstractValidator<CreateContractDto>
{
    public CreateContractDtoValidator()
    {
        RuleFor(dtoModel => dtoModel.UserId).NotEmpty()
            .WithMessage("User_id should not be null");
        RuleFor(dtoModel => dtoModel.ProductCount).NotEmpty()
            .WithMessage("Product count should not be equql to null or 0");
        RuleFor(dtoModel => dtoModel.ProductId).NotEmpty();
        RuleFor(dtoModel => dtoModel.ProducProperties).NotEmpty();
    }
}
