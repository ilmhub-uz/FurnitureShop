using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators
{
    public class UpdateOrganizationDtoValidator : AbstractValidator<UpdateOrganizationDto>
    {
        public UpdateOrganizationDtoValidator()
        {
            RuleFor(updateorganizationdto => updateorganizationdto.Name).NotNull();
            RuleFor(updateorganizationdto => updateorganizationdto.Name)
           .MaximumLength(25).MinimumLength(3).When(updateorganizationdto => updateorganizationdto.Name is not null);
        }
    }
}
