using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators
{
    public class AddEmployeeDtoValidator : AbstractValidator<AddEmployeeDto>
    {
        public AddEmployeeDtoValidator()
        {
            RuleFor(dto => dto.OrganizationId).NotEmpty().NotNull();

            RuleFor(dto => dto.ERole).NotNull().IsInEnum();

            RuleFor(dto => dto.Email).NotEmpty().NotNull().EmailAddress().Length(5, 100).WithMessage("This is not valid email");
        }
    }
}
