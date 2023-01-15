using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class CreateOrderProductDtoValidator : AbstractValidator<CreateOrderProductDto>
    {
        public CreateOrderProductDtoValidator() 
        {
            RuleFor(createorderproductdto => createorderproductdto.ProductId)
                .NotNull().WithMessage("productid is not valid");
            RuleFor(createorderproductdto => createorderproductdto.ProductId)
                .NotEqual(default(Guid));
            RuleFor(createorderproductdto => createorderproductdto.Count)
                .NotNull().NotEqual(default(uint));
        }
    }
}
