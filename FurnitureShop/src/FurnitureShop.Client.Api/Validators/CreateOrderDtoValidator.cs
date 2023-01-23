using FluentValidation;
using FurnitureShop.Client.Api.Dtos;

namespace FurnitureShop.Client.Api.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(createorderdto => createorderdto.Products).NotEmpty();
           
            RuleForEach(createOrderDto => createOrderDto.Products)
           .ChildRules(products =>
           {
               products.RuleFor(p => p.ProductId).NotNull().NotEqual(default(Guid));
               products.RuleFor(p => p.Count).NotNull().NotEqual(default(uint));
           });
        }
    }
}
