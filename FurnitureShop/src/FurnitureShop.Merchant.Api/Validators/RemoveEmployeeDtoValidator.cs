using FluentValidation;
using FurnitureShop.Merchant.Api.Dtos;

namespace FurnitureShop.Merchant.Api.Validators
{
    public class RemoveEmployeeDtoValidator : AbstractValidator<RemoveEmployeeDto>
    {
        public RemoveEmployeeDtoValidator()
        {
            RuleFor(dto => dto.EmployeeId).NotEmpty().NotNull();
            RuleFor(dto => dto.OrganizationId).NotEmpty().NotNull();
        }
    }
}
